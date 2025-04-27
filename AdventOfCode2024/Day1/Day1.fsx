// https://adventofcode.com/2024/day/1

module Part1 =
    open System.IO

    let solve fileName =
        let dir = __SOURCE_DIRECTORY__
        let inFile = $"{dir}/{fileName}"
        let fileContents = File.ReadAllLines inFile

        let parseLine (line: string) =
            line.Split("   ") |> Array.map int |> (fun a -> (a.[0], a.[1]))

        let distance pair = fst pair - snd pair |> abs

        let left, right = fileContents |> Array.map parseLine |> Array.unzip

        Array.zip (Array.sort left) (Array.sort right)
        |> Array.map distance
        |> Array.sum

    let exampleResult = solve "example_input.txt"
    let fullResult = solve "input.txt"
