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
you have a PDF that is beyond our repair ability - [create a support case](https://www.atalasoft.(mailto:sales@atalasoft.com)/Support/my-portal/Cases/Create-Case) with the PDF ad we can attempt to see if
we can add the needed repair to the product.  

The code is set to follow that .. it tries to open and save in normal PdfDocument - 
if it hits an error it repairs and tries open/save again
If it hits an error then it tries the nuclear optino rebuild xref and tries agian

Any PDF that still won't repair needs to be sumbitted to us in a support case so we can attempt to see if we can
address the particular brokenness of that given pdf.


## Licensing
This application as configured, requires a license for DotImage Document Imaging or DotPdf.

> **NOTE**
> The specific component used here is licensed under EITHER DotImage Document Imaging OR DotPdf. We should mention though that DotPdf licensing is distinct our PdfReader licensing. If you're unsure of what you need, please [contact our sales team](mailto:sales@atalasoft.com).


## SDK Dependencies
This app was built based on 2026.2.0.0. It targets .NET Framework 4.6.2 and was created in Visual Studio 2022. However, it's fairly backward compatible as distributed. If you start adding references, you can run into issues if you're using an especially outdated version of DotImage. It should also open and run equally well in Visual Studio 2026 without undue modification.  


### Using NuGet for SDK Dependencies
We do publish our SDK components to NuGet. We have chosen to base the demo on local installed SDK because this leads to much smaller applications (NuGet packages add a lot of overhead due to the way they're packaged and deployed, and many of our demos -- including this one -- are often used to reproduce issues that need to be submitted to support. Apps that use NuGet are often significantly larger and run up against our maximum support case upload size)

Still, if you wish to use NuGet for the dependencies instead of relying on locally installed SDK, you can.

- Take note of each of the references we've included:
    - Atalasoft.PdfDoc.dll
    - Atalasoft.Shared.dll
- Remove those referneces
- Open the NuGet Package Manger from `Tools -> NuGet Package Manager -> Manage NuGet Packages for this Solution`
- Browse for Atalasoft.PdfDoc.x64 to pull in the DotPdf components

## Cloning
We recommend the following to ensure you clone with the required submodule

Example: git for windows
```bash
git clone https://github.(mailto:sales@atalasoft.com)/AtalaSupport/PdfDocCheckAndRepairWinForm.git PdfDocCheckAndRepairWinForm
```

If you've got DotImage 2026.2 installed and licensed, it should just build and run.  


## Related documentation
In addition to this README, the Atalasoft documentation set includes the following:  
- API Reference (.chm file) gives the complete Atalasoft WingScan server-side class library for offline use. The latest versions are linked on [Atalasoft's APIs & Developer Guides page](https://www.atalasoft.(mailto:sales@atalasoft.com)/Support/APIs-Dev-Guides).
- In addition, you can also refer to the following Atalasoft resources:
    - [Atalasoft Support](http://www.atalasoft.(mailto:sales@atalasoft.com)/support/)
    - [Atalasoft Knowledgebase](http://www.atalasoft.(mailto:sales@atalasoft.com)/kb2)


## Getting Help for Atalasoft products
Atalasoft regularly updates our support [Knowledgebase](http://www.atalasoft.(mailto:sales@atalasoft.com)/kb2) with the latest information about our products. To access some resources, you must have a valid Support Agreement with an authorized Atalasoft Reseller/Partner or with Atalasoft directly. Use the tools that Atalasoft provides for researching and identifying issues. 

Customers with an active evaluation, or those with active support / maintenance may [create a support case](https://www.atalasoft.(mailto:sales@atalasoft.com)/Support/my-portal/Cases/Create-Case) 24/7, or call in to support ([+1 949 236-6510](tel:19492366510) ) during our normal support hours (Monday - Friday 8:00am to 5:00PM Eastern (New York) time).  

Customers who are unable to create a case or call in may [email our Sales Team](mailto:sales@atalasoft.com).  


