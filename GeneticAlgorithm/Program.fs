// F# の詳細については、http://fsharp.org を参照してください
// 詳細については、'F# チュートリアル' プロジェクトを参照してください。

open System
open System.IO
open System.Text
open System.Windows.Forms
open System.Drawing
open LibGeneticAlgorithm

[<EntryPoint>]
let main argv = 
    let start = 0xF0AB9D
    let target = 0x020503
    let al = GeneticAlgorithm (start, target)
    
    let outPath = @"C:\Users\孝輔\GeAlOut.txt"

    let outs = ref ""
    let count = ref 1
    let rec loop () =
        let (distance, color) = al.NextGeneration ()
        let out = String.Format ("#{0:0000}\tdst = {1:000.000000}\tclr = {2:000000}", !count, distance, color.ToString "X6")
        printfn "%s" <| out
        outs := !outs + "\r\n" + out
        count := !count + 1
        if distance = 0. then 
            printfn "***Process end***"
        else 
            loop ()
    
    loop ()
    File.WriteAllText (outPath, !outs)

    Console.ReadKey() |> ignore
    0 // 整数の終了コードを返します
