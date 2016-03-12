---

# MBrace Hack Day!

![alt](images/mbrace.png)

### Isaac Abraham 

![alt](images/CIT-vertical.png)

---

##Who am I?

* .NET dev / contractor / consultant since .NET 1.0
* Director of Compositional IT
* F# developer since 2012
* Microsoft MVP in F# / .NET
* Based in Hessen & London


* email *[isaac@compositional-it.com](mailto:isaac@compositional-it.com)*
* twitter *[@isaac_abraham](https://twitter.com/isaac_abraham)*
* blog *http://cockneycoder.wordpress.com*

***

## Today's aims
* Learn how to use MBrace
* Learn a bit of F#
* Learn a bit about Azure

---

## Morgen
* Overview of MBrace
* Set of structured lessons
* Local development

' lessons will take you through the different areas of MBrace
' MBrace has a locally hosted mode. This afternoon we'll try to use MBrace.Azure!

---

## Nachmittag
* Data Hack
* Work with real data sets
* Cluster hosted in Azure
* Share results at the end of the day     

***

## What is MBrace

* General framework for distribution
* Compute, data or IO -bound workloads
* Works with any .NET language
* Written in F# (with recent C# SDK)

---

## Use cases

* Batch Processing
    * Distributed compute workloads e.g. maths, finance
    * Big data workloads - unstructured data, ETLs etc.

* Real time & In Process
    * Simple "offloading" of background tasks

* Load testing
* "what-if" analysis
* usw usw usw :)

---

## F#? C#? WTF#?

* Why should you use F#?
* Why should you use F# with MBrace?
* You *can* use the C# SDK, but it's not as rich

' REPL
' Lightweight syntax
' Immutability
' cloud { }

---

## F# Primer in < 5 minutes

---

### Values

    // bind 5 to x
    let x:int = 5
    
    // type inference
    let x = 5
    
    // functions are just values
    let helloWorld (name) = printfn "Hello, %s" name 

---

### Types
    
    // Tuples are first class citizens in F#
    let person = Tuple.Create("Isaac", 36)
    let personShortHand = "isaac", 36

    // Declaring a record
    type Person = { Name : string; Age : int }
    
    // Create an instance
    let me = { Name = "Isaac"; Age = 36 }

---

### ACHTUNG!
    open System
     
    // No curly braces - space sensitive!
    let prettyPrintTime() =
        let time = DateTime.UtcNow
        printfn "It is now %d:%d" time.Hour time.Minute

    let x = 5 // immutable by default!
    let mutable y = 10
    y <- 20 // ok 

***

* Core Concepts of MBrace
    * cloud { }
    * CloudValue
    * CloudFile
    * CloudFlow

***
    
# Azure
    * Storage
    * Service Bus

* Resources
    * MBrace Starter Kit
    * Azure Passes
    * Datasets