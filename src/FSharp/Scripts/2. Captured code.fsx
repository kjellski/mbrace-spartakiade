#load "SetupThespian.fsx"

open System
open System.Diagnostics
open MBrace.Core
open MBrace.Thespian

let cluster = createLocalCluster(4)

// 1. Here's a simple function. We want to carry this out on the cluster.
let addNumbers(a, b) =
    printfn "Adding %d and %d" a b
    a + b

// Easy - wrap it in cloud { }. MBrace will send across the function to the node as a compiled
// .NET assembly automatically.
let answer =
    cloud {
        return addNumbers(5, 10)
    } |> cluster.Run

// Where is the Console output? Check the worker output windows!





// 2. MBrace will also automatically capture objects and send across to the cluster
let firstNumber = 10
let otherAnswer = cloud { return addNumbers(firstNumber, 5) } |> cluster.Run





// 3. We can also call out to any .NET assembly within a cloud { }
let answerFromCSharp =
    cloud {
        let calc = CSharp.Calculator()
        return calc.Add(firstNumber, 5)
    } |> cluster.Run


// 4. One for you. Make a cloud computation to get the current geolocation. CSharp project
// has the geolocation code already.

// tip ... to go from a Task<T> to Cloud<T>, you need to use Cloud.AwaitTask
let location =    
    cloud {
        let! geoLocation = CSharp.GeoLocation.GetLongLat "google API key" |> Cloud.AwaitTask
        return geoLocation
    } |> cluster.Run

