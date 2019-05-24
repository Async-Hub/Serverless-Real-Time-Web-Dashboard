// Learn more about F# at http://fsharp.org

open System
open System.Threading
open Microsoft.AspNetCore.SignalR.Client

[<EntryPoint>]
let main argv =
    Thread.Sleep(3000)

    let builder = new HubConnectionBuilder()
    builder.WithUrl("http://localhost/api/") |> ignore
    
    let connection = builder.Build()

    connection.On<string>("ReceiveMessage", fun message -> Console.WriteLine(message)) |> ignore

    connection.StartAsync() |> Async.AwaitTask |> ignore

    let connectionState = connection.State.ToString()
    printf "SignalR connection state: %s \n" connectionState

    let mutable continueLooping = true
    while continueLooping do
        let key = Console.ReadKey(true)
        if key.Key = ConsoleKey.X then
         continueLooping <- false

    0 // return an integer exit code
