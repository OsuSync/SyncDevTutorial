通常来说，一个插件类内含一个**事件绑定器**`EventBus`,通常可以收发各种实现IPluginEvent接口的类的消息.Sync本体本身提供`PluginEvents`类，里面包含了各种预置的插件(程序)初始化事件.
<br>建议通过构造方法以及重写`OnEnable()`来绑定事件.

``` CSharp
public override void OnEnable()
{
    base.OnEnable();

    //绑定各种事件并注册各种功能,详情 https://github.com/OsuSync/Sync/blob/master/Sync/Plugins/PluginManager.cs#L23-L135
    EventBus.BindEvent<PluginEvents.InitPluginEvent>(OnPluginInit);


    EventBus.BindEvent<PluginEvents.LoadCompleteEvent>(OnAllPluginLoadedFinish);
}

private void OnPluginInit(PluginEvents.InitPluginEvent e)
{
    logger.LogInfomation("MySimplePlugin初始化~");
}

private void OnAllPluginLoadedFinish(PluginEvents.LoadCompleteEvent e)
{
    logger.LogInfomation("Sync插件已经全部加载完毕，这里可以引用其他插件来进行操作");
}
```

当然如果想自己注册一个事件，可以实现IPluginEvent接口，当必要时直接调用EventBus.RaiseEvent<IPluginEvent>()或者EventBus.RaiseEventAsync<IPluginEvent>()来触发事件：

``` CSharp
//MySimpleEvent.cs
public class MySimpleEvent : IPluginEvent
{
    public MySimpleEvent(string message)
    {
       Message = message;
    }

    public string Message { get; }
}
```

``` CSharp
//MySimplePlugin.cs 

public override void OnEnable()
{
    base.OnEnable();

    EventBus.BindEvent<PluginEvents.InitCommandEvent>(OnCommandInit);

    //绑定自定义事件
    EventBus.BindEvent<MySimpleEvent>(OnMySimpleEvent);
}

private void OnCommandInit(PluginEvents.InitCommandEvent e)
{
    //绑定一个命令
    e.Commands.Dispatch.bind("mhnb",HandleCommands, "这是一个命令");
}

private bool HandleCommands(Arguments args)
{
    foreach (string param in args)
    {
        logger.LogInfomation("param:"+param);
    }

    //发送MySimpleEvent事件，将调用各个绑定到此事件的回调
    EventBus.RaiseEvent/*EventBus.RaiseEventAsync*/(new MySimpleEvent("mhnbbbbbbbbbbbbbbb"));

    return true;
}

private void OnMySimpleEvent(MySimpleEvent e)
{
    logger.LogInfomation(e.Message);
}
```