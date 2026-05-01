# PdfDocCheckAndRepairWinForm
This is a quick way to "sanity check" a PDF.

You can run this app and it will let you browse to find a PDF, Open the PDF and it will first use PdfDocument.Examner.ExaminerResults to check on some basic info and report.

THen hit ENTER for it to open the PDF in PDFDocucmnet and then save.

If it encounters any issues during the test save, it will then attempt a repair.  

The repair is set to fire events to show when it encounters errors and what it does about them etc.. 
useful if you really want to know what it's doing to repair.  

Really though, to avoid all of this you can just use:

```csharp
PdfDocument doc = new PdfDocument(streamOrFileNameHere, new RepairOptions());
```

This automatcally repairs only if needed and fixes most bad pdfs without need for input.  

## ALSO NOTE
There is one option

```csharp
opts.StructureOptions.ForceRebuildCrossReferenceTable = true;
```

If it fials to repair under "normal" repair it will retry with this option set.

This is not recommended for routine repair and should only ever be used when regular repair fails, as it is a much "heavier touch" fully regenerating the xref table, taking much more time/processing and causing large number of structurl changes.  

At the end, if the repair has worked then you have a damaged PDF that we were able to repair. If it still can't fix it
you have a PDF that is beyond our repair ability - [create a support case](https://www.atalasoft.com/Support/my-portal/Cases/Create-Case) with the PDF ad we can attempt to see if
we can add the needed repair to the product.  

The code is set to follow that .. it tries to open and save in normal PdfDocument - 
if it hits an error it repairs and tries open/save again
If it hits an error then it tries the nuclear optino rebuild xref and tries agian

Any PDF that still won't repair needs to be sumbitted to us in a support case so we can attempt to see if we can
address the particular brokenness of that given pdf.



