﻿#load "SetupThespian.fsx"
#load @"..\GeoCoder.fs"

open System
open System.Diagnostics
open MBrace.Core
open MBrace.Thespian
open MBrace.Library.Cloud
open System.IO
open System.Net

let cluster = createLocalCluster(4)

// CloudFile is an API very similar to System.IO.File. With Thespian, files go into your temp folder.

// 1. Create a file in the cluster's data store.
CloudFile.WriteAllText("file.txt", "Hello world!") |> cluster.Run

// 2. Try to read it back using CloudFile.ReadAllText

// 3. Upload the "Hello World.fsx" file into the cluster.
let path = Path.Combine(__SOURCE_DIRECTORY__, "1. Hello World.fsx")

// 4. Download the contents from an HTTP stream directly into the cluster (not via the local script)
// Then return the size of the file that was downloaded.
let size =
    cloud {
        let stream = (new WebClient()).OpenRead "https://tse3.mm.bing.net/th?id=OIP.M63e6e64b77055e85c20e2752d1cbb5d3H0&w=223&h=125&c=7&rs=1&qlt=90&o=4&pid=1.1"
        // fill in the rest, using CloudFile.UploadFromStream and CloudFile.GetSize
        do! CloudFile.UploadFromStream(@"userData\file1.jpg", stream, true) |> Cloud.Ignore
        let! fileInfo = CloudFile.GetInfo("file1.jpg")
        return fileInfo.Size
    } |> cluster.Run

// 5. Upload the following books into the file store

let books =
    [ "aliceinwonderland.txt"; "huckleberryfinn.txt"; "prideprejudice.txt" ]
    |> List.map(fun file -> Path.Combine(__SOURCE_DIRECTORY__, @"..\data\", file) |> Path.GetFullPath)

// use the CloudFile.Upload function
let uploadedBooks =
    cloud {
        do! CloudFile.Upload(books, @"books") |> Cloud.Ignore
    } |> cluster.Run