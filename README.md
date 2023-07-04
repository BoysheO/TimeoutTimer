# TimeoutTimer
A simple and useful to timeout logic.Help for throw TimeoutException.

| Build | NuGet |
|--|--|
|![](https://github.com/BoysheO/TimeoutTimer/workflows/nuget/badge.svg)|[![](https://img.shields.io/nuget/v/TimeoutTimer.svg)](https://www.nuget.org/packages/TimeoutTimer)|

# User's guidance 
 这个东西设计的时候,一眼便可看懂。因此只需要打开就可看明白代码.

它是为Task中的超时机制设计的,如果使用CancellationToken来设定超时时间,超时时抛出OperationCanceledException,很难追踪调试错误.

因此诞生了这个玩意.一般设计一个可超时的Task,其API应形如
````csharp
async Task Foo(float timeoutSec=30f){
    var tt=new TimeoutTimer(timeoutSec);
    await DoSth(tt.RemainingSec);
    tt.ThrowIfTimeout();//if timeout,TimeoutException will be thrwon.
}

async Task DoSth(flat timeoutSec){}

````
！不应将TimeoutTimer传递到其他Task去,只传递它的RemingSec，否则不利于调试.  
！出于历史原因，时间源采用的是DateTimeOffset.Now，这样的话在死循环中会永不超时。因此不可作为调试工具使用，并且此库本意仅仅是为简化超时业务处理，而不是作逻辑调试。要进行调试，请使用Stopwatch 。
