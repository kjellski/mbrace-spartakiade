[<AutoOpen>]
module SetupThespian

// Remember, highlight multiple lines and hit LALT + ENTER to send them to F# Interactive
#load @"load-references-debug.fsx"
open System.IO
open MBrace.Thespian

ThespianWorker.LocalExecutable <- Path.Combine(__SOURCE_DIRECTORY__, @"..\..\..\packages\MBrace.Thespian\tools\mbrace.thespian.worker.exe")

let createLocalCluster nodes =
    ThespianCluster.InitOnCurrentMachine(nodes,
                                         logger = new ConsoleLogger(), 
                                         logLevel = LogLevel.Info)