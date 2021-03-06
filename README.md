
工程目的：观察别人的商业级项目的大致内容，常规需求组成，框架写法，实际上有很多地方都用不上，或者不适配。

# UI方面

## UI资源的加载

由于热更方面的机制，所以很多组件都是通过find，然后手动赋值。

unity组件：根据预制体层级目录动态获取,transform.FindComponent<T>在某个transform下获取，缩小搜索范围。

UGUI监听事件或者NGUI监听事件注册，在获取到组件后注册上去。

其余一些结构体参数的处理如下：

## 事件的分发机制

仿造MVC框架，但是M和C的界限并不明显

>* UI的Model基类
```
public class UIBaseModel
{
    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="key"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    public void DispatchValueUpdateEvent(string key, object oldValue, object newValue)
    {
        EventHandler<ValueChangeArgs> handler = ValueUpdateEvent;
        if (handler != null)
        {
            handler(this, new ValueChangeArgs(key, oldValue, newValue));
        }
    }
    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="args"></param>
    public void DispatchValueUpdateEvent(ValueChangeArgs args)
    {
        EventHandler<ValueChangeArgs> handler = ValueUpdateEvent;
        if (handler != null)
        {
            handler(this, args);
        }
    }
    public event EventHandler<ValueChangeArgs> ValueUpdateEvent; //模块事件动态注册，这里使用的是EventHandler，可以免去定义各种名字的void返回委托，而实际的方法意义由ValueChangeArgs内的参数决定
}
```
>* 委托参数ValueChangeArgs
```
public class ValueChangeArgs : EventArgs
{
    public string key { set; get; }
    public object oldValue { set; get; }
    public object newValue { set; get; }
    public ValueChangeArgs(string key, object oldValue, object newValue)
    {
        this.key = key;
        this.oldValue = oldValue;
        this.newValue = newValue;
    }
    public ValueChangeArgs(string key)
    {
        this.key = key;
    }
    public ValueChangeArgs()
    {
    }
}
```

这个类记录下了老值和新值，最主要的部分还是key，因为需要根据key来匹配事件分发中实际需要调用的方法。

使用这种事件分发机制的好处是，我们可以把精力主要放在触发入口，而实际上触发的函数在通过key的筛选后才会运行，写代码的人只要处理好某个功能块具体的实现，而不需要去关注什么时候触发。无论是网络模块的需求，还是具体逻辑的需求，他们之间的代码不会被干扰，整体结构很干净，代码很清晰。


具体实例 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

定义key
```
public class UIGameModelConst
{
    public const string KEY_RefreshRoomInfo = "RefreshRoomInfo"; //刷新房间信息
}
```

定义Model
```
public class UIGameModel : UIBaseModel
{
 	private RoomInfo mRoomInfo;
    public RoomInfo RoomInfo
    {
        get
        {
            return mRoomInfo;
        }
        set
        {
            ValueChangeArgs ve = new ValueChangeArgs(UIGameModelConst.KEY_RefreshRoomInfo, mRoomInfo, value);
            mRoomInfo = value;
            DispatchValueUpdateEvent(ve);
        }
    }
}
```
上述代码中当RoomInfo被赋值时，分发事件。（所有的事件在V层就已经注册到了UIGameModel.ValueUpdateEvent）

在V层的注册代码可能会像这样：

```
private void OnValueUpdateEvent(object sender, ValueChangeArgs e)
{
    switch (e.key)
    {
        case UIGameModelConst.KEY_RefreshRoomInfo:
            RefreshRoomInfo((RoomInfo)e.newValue);
            break;
    }
}
```

在一个model实例中可能会注册很多方法，每个方法都对应了一个key，通过调用model实例的某一处触发事件分发，所有的事件都会被派发，最后会在被分发的事件中筛选。



## 事件分发器

key值对应的一系列委托。

根据逻辑需要，注册的时候询key注册，分发的时候也是询key分发，注销部分委托依然是询key注销。

只需要这样定义就可以了。

```
private Dictionary<string, Delegate> mEventMap = new Dictionary<string, Delegate>();
```
