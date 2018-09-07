namespace Samples

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.MediumEditor
open WebSharper.UI
open WebSharper.UI.Html
open WebSharper.UI.Client
open WebSharper.UI.Templating

[<Require(typeof<WebSharper.MediumEditor.Resources.DefaultCSS>)>]
[<JavaScript>]
module HelloWorld =

    type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>

    let editedText = Var.Create "Hello blogger!"

    [<SPAEntryPoint>]
    let Main() =
        IndexTemplate.Main()
            .Preview(editedText.View.Doc(Doc.Verbatim))
            .Elt()
            .OnAfterRender(fun _ ->
                let editor = MediumEditor(".editable")
                JQuery.Of(".medium-editor-element").Keyup(fun el _ ->
                    Var.Set editedText <| (JQuery.Of("#formatted").Val() :?> string)
                ).Ignore
                JQuery.Of(".medium-editor-element").On("input", fun el _ ->
                    Var.Set editedText <| (JQuery.Of("#formatted").Val() :?> string)
                ).Ignore
                ()
            )
        |> Doc.RunById "main"
