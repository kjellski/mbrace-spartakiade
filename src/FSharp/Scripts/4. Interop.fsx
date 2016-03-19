#load "SetupThespian.fsx"

open System
open System.Diagnostics
open System.Net
open Newtonsoft.Json
open MBrace.Core
open MBrace.Thespian

let cluster = createLocalCluster(4)

// 1. We can also call out to any .NET assembly within a cloud { }
let numberInFSharp = 10

let answerFromCSharp =
    cloud {
        let calc = CSharp.Calculator()
        return calc.Add(numberInFSharp, 5)
    } |> cluster.Run

type Location = { lat: double; lng: double }
type LocationResult = { accuracy: double; location: Location }

// 2. One for you. Make a cloud computation to get the current geolocation. CSharp project
// has the geolocation code already.
let geoLocator key =
    async {
        let webClient = new WebClient()
        let data = "{ \"considerIp\": \"true\" }"
        let uri = new Uri("https://www.googleapis.com/geolocation/v1/geolocate?key=" + key)
        let! resultString = webClient.UploadStringTaskAsync(uri, data) |> Async.AwaitTask
        let result = JsonConvert.DeserializeObject<LocationResult>(resultString)
        return result.location.lat, result.location.lng
    }

let googleKey = "AIzaSyAf-uZ-6xCoXdrFUZOYhNS-9cy0qFMDZYg"

// tip ... to go from a Task<T> to Cloud<T>, you need to use Cloud.AwaitTask
let location =
    cloud {
        // CSharp.GeoLocation.GetLongLat("google API key")
        let! latlng = geoLocator googleKey |> Cloud.OfAsync
        return latlng
    } |> cluster.Run