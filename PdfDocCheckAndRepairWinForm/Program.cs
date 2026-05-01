using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Atalasoft.Imaging;
//using Atalasoft.Imaging.Codec;
//using Atalasoft.Imaging.Codec.Pdf;
using Atalasoft.PdfDoc;
using System.Windows.Forms;
using Atalasoft.PdfDoc.Repair;
using System.IO;
using Atalasoft.PdfDoc.Examiner;
using Atalasoft.PdfDoc.Generating.Validation;
using Atalasoft.PdfDoc.Generating;

namespace PdfDocCheckAndRepairWinForm
{
    class Program
    {
        static System.Security.SecureString userPass;
        static System.Security.SecureString ownerPass;

        [STAThread]
        static void Main(string[] args)
        {
            // Add PdfDecoder to RegisteredDecoders.Decoders
            //RegisteredDecoders.Decoders.Add(new PdfDecoder() { Resolution = 200, RenderSettings = new RenderSettings() { AnnotationSettings = AnnotationRenderSettings.RenderNone } });

            Console.WriteLine("PdfDocCheckAndRepairWinForm Starting...");
            string imgPath = GetWorkingDir();
            string inFile = imgPath + "target.pdf";

            userPass = new System.Security.SecureString();
            ownerPass = new System.Security.SecureString();


            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                //dlg.FileName = inFile;
                dlg.InitialDirectory = @"C:\_DATA\_CUSTOMER_PROJECTS\2023\";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    inFile = dlg.FileName;
                }
                else
                {
                    inFile = null;
                }
            }


            if (inFile == null)
            {
                Console.WriteLine("No input document selected... nothing to do.");
            }
            else
            {
                Console.WriteLine("  inFile: " + inFile);



                Console.WriteLine("BEGIN Processing...");


                Console.WriteLine("\n\n================================================");
                Console.WriteLine("PdfDoc.Examiner");
                try
                {

                    using (FileStream fs = new FileStream(inFile, FileMode.Open))
                    {
                        //ExaminerResults res = ExaminerResults.FromStream(fs, "", "");
                        ExaminerResults res = ExaminerResults.FromStream(fs);


                        Console.WriteLine("  DocumentVersion: " + res.DocumentVersion);
                        Console.WriteLine("  ErrorsEncountered... ");
                        if (res.ErrorsEncountered != null && res.ErrorsEncountered.Count > 0)
                        {
                            foreach (string err in res.ErrorsEncountered)
                            {
                                Console.WriteLine("    " + err);
                            }
                        }
                        else
                        {
                            Console.WriteLine("    none");
                        }
                        Console.WriteLine("  FormUsesXFA: " + res.FormUsesXFA);
                        Console.WriteLine("  HasForm: " + res.HasForm);
                        Console.WriteLine("  HasSignatures: " + res.HasSignatures);
                        Console.WriteLine("  HasXmp: " + res.HasXmp);
                        Console.WriteLine("  IsBadlyDamaged: " + res.IsBadlyDamaged);
                        Console.WriteLine("  IsEncrypted: " + res.IsEncrypted);
                        Console.WriteLine("  IsIncorrectVersionForXrefStream: " + res.IsIncorrectVersionForXrefStream);
                        Console.WriteLine("  IsPdf: " + res.IsPdf);
                        Console.WriteLine("  IsPdfA: " + res.IsPdfA);
                        Console.WriteLine("  IsPdfPortfolio: " + res.IsPdfPortfolio);
                        Console.WriteLine("  IsValidPdfHeader: " + res.IsValidPdfHeader);
                        if (res.Metadata != null)
                        {
                            Console.WriteLine("  Metadata:");
                            if (res.Metadata.Author != null)
                            {
                                Console.WriteLine("    Author: " + res.Metadata.Author);
                            }
                            else
                            {
                                Console.WriteLine("    Author: null");
                            }

                            if (res.Metadata.CreationDate != null)
                            {
                                Console.WriteLine("    CreationDate: " + res.Metadata.CreationDate.ToString());
                            }
                            else
                            {
                                Console.WriteLine("    CreationDate: null");
                            }

                            if (res.Metadata.Creator != null)
                            {
                                Console.WriteLine("    Author: " + res.Metadata.Creator);
                            }
                            else
                            {
                                Console.WriteLine("    Creator: null");
                            }

                            if (res.Metadata.CustomFields != null)
                            {
                                Console.WriteLine("    CustomFields: " + res.Metadata.CustomFields.Count.ToString());
                            }
                            else
                            {
                                Console.WriteLine("    CustomFields: null");
                            }


                            if (res.Metadata.DocumentTrapping != null)
                            {
                                Console.WriteLine("    DocumentTrapping: " + res.Metadata.DocumentTrapping.ToString());
                            }
                            else
                            {
                                Console.WriteLine("    DocumentTrapping: null");
                            }

                            if (res.Metadata.Keywords != null)
                            {
                                Console.WriteLine("    Keywords: " + res.Metadata.Keywords.Length.ToString());
                            }
                            else
                            {
                                Console.WriteLine("    Keywords: null");
                            }


                            if (res.Metadata.ModificationDate != null)
                            {
                                Console.WriteLine("    ModificationDate: " + res.Metadata.ModificationDate.ToString());
                            }
                            else
                            {
                                Console.WriteLine("    ModificationDate: null");
                            }

                            if (res.Metadata.Producer != null)
                            {
                                Console.WriteLine("    Producer: " + res.Metadata.Producer.ToString());
                            }
                            else
                            {
                                Console.WriteLine("    Producer: null");
                            }

                            if (res.Metadata.Subject != null)
                            {
                                Console.WriteLine("    Subject: " + res.Metadata.Subject.ToString());
                            }
                            else
                            {
                                Console.WriteLine("    Subject: null");
                            }

                            if (res.Metadata.Title != null)
                            {
                                Console.WriteLine("    Title: " + res.Metadata.Title.ToString());
                            }
                            else
                            {
                                Console.WriteLine("    Title: null");
                            }



                        }
                        else
                        {
                            Console.WriteLine("  Metadata: NULL");
                        }
                        Console.WriteLine("  PageCount: " + res.PageCount);
                    }
                }
                catch (Atalasoft.PdfDoc.IncorrectPasswordException ex)
                {
                    Console.WriteLine("  INCORRECT PASSWORD");
                }


                Console.WriteLine("... hit ENTER when ready ...");
                Console.ReadLine();

                //Console.WriteLine("\n\n================================================");
                //Console.WriteLine("Opening inFile as AtalaImage");
                //AtalaImage img = new AtalaImage(inFile, null);
                //img.Dispose();

                Console.WriteLine("\n\n================================================");
                Console.WriteLine("Opening inFile as PdfDoc");
                try
                {
                    //// CODE TO FORCE ERROR TO FORCE REPAIR
                    //int denom = 0;
                    //double foo = 1 / denom;

                    
                        PdfDocument doc = new PdfDocument(inFile, userPass, ownerPass);
                        Console.WriteLine("Saving testSave version of  inFile");

                        doc.AllowSavingOfPreviouslySignedDocuments = true;
                        doc.Save(inFile + ".testSave.11.5.pdf", new PdfSaveOptions() {  UseCompressedObjectStreams = true, PdfASavingBehavior = Atalasoft.PdfDoc.PdfA.PdfASavingBehavior.SavePdf });
                        //doc.Save(inFile + ".testSave.11.5.pdf");
                        //doc.Close();
                        //int z = 0;
                        //double foo = 1 / z;
                    
                    //// UNCOMMENT TO FORCE REPAIR ALWAYS
                    //int zero = 0;
                    //int foo = 3 / zero;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine(ex.GetType().ToString());
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine("------------------------------------------");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.GetType().ToString());
                        Console.WriteLine(ex.InnerException.Message);
                        Console.WriteLine(ex.InnerException.StackTrace);
                        Console.WriteLine("------------------------------------------");
                    }


                    Console.WriteLine(" PRESS ENTER TO CONTINUE ");
                    Console.ReadLine();


                    if (ex.Message.Contains("Invalid character in hex string: \0") || (ex.InnerException != null && ex.InnerException.Message.Contains("Invalid character in hex string: \0")))
                    {
                        Console.WriteLine("\nSPECIAL FIX FOR INVALID CHARACTER AT HEX STRING \\0");

                        // SPECIAL fix code for invalid char at hex string \0
                        bool fixForHexCharStatus = FixInvalidCharAtHexString(inFile, inFile + ".hexStringFixed1.11.4.pdf");

                        Console.WriteLine("\nSPECIAL FIX FINAL STATUS: " + fixForHexCharStatus.ToString());
                        Console.WriteLine();
                    }
                    else
                    {
                        RepairOptions opts = new RepairOptions();
                        ////These are some additional structural repairs that could be added if desired
                        /// they're commented out because the standard opts are often best
                        ///  Separated into those which default to false and true for easy reference
                        /// DEFAULTS TO FALSE
                        opts.StructureOptions.ForceRebuildCrossReferenceTable = false;

                        /// DEFAULT TO TRUE
                        //opts.StructureOptions.RestoreOrphanedPages = false;
                        //opts.StructureOptions.RepairNameTrees = true;
                        //opts.StructureOptions.AllowPartialNameTrees = true;
                        //opts.StructureOptions.CorrectInvalidDataStreamLengths = true;

                        //opts.StructureOptions.CreateBlankPageIfNoPagesFound = ?
                        //opts.StructureOptions.DestinationAction = ?
                        //opts.StructureOptions.DuplicateNameTreeEntryRepairAction = ?



                        // this is the default maximum severity
                        opts.MaximumAllowableSeverity = Atalasoft.PdfDoc.Generating.Validation.Severity.Major;

                        opts.ProblemEncountered += new EventHandler<Atalasoft.Shared.Validation.ProblemEventArgs>(opts_ProblemEncountered);
                        opts.ProblemSkipped += new EventHandler<Atalasoft.Shared.Validation.ProblemEventArgs>(opts_ProblemSkipped);
                        opts.ProblemRepaired += new EventHandler<Atalasoft.Shared.Validation.ProblemEventArgs>(opts_ProblemRepaired);

                        Console.WriteLine("Attempting fix... ");

                        //PdfDocument.Repair(inFile, inFile + ".fixed.11.2.pdf", opts);

                        // SPECIAL fix code to try for odd double EOF / invalid char at hex string
                        //DoStreamFix(inFile, inFile + ".streamfix.pdf");
                        try
                        {
                            //PdfDocument repairDoc = new PdfDocument(null, null, inFile, opts);
                            PdfDocument repairDoc = new PdfDocument(inFile, userPass, ownerPass, opts);
                            repairDoc.AllowSavingOfPreviouslySignedDocuments = true;
                            repairDoc.Save(inFile + ".fixed.11.5.pdf", new PdfSaveOptions() { PdfASavingBehavior = Atalasoft.PdfDoc.PdfA.PdfASavingBehavior.SavePdf });

                            Console.WriteLine("fixed as " + inFile + ".fixed.11.5.pdf");
                            Console.WriteLine("Opening fixed document");

                            PdfDocument doc = new PdfDocument(inFile + ".fixed.11.5.pdf");
                            doc.Close();
                        }
                        catch (Exception ex2)
                        {
                            Console.WriteLine("EXCEPTION ENCOUNTERED::");
                            Console.WriteLine(ex2.GetType().ToString());
                            Console.WriteLine(ex2.Message);
                            Console.WriteLine(ex2.StackTrace);
                            Console.WriteLine("\n");
                            if (ex2.InnerException != null)
                            {
                                Console.WriteLine("    INNER EXCEPTION:");
                                Console.WriteLine("    " + ex2.InnerException.GetType().ToString());
                                Console.WriteLine("    " + ex2.InnerException.Message);
                                Console.WriteLine(ex2.InnerException.StackTrace);
                                Console.WriteLine("\n");
                            }
                            Console.WriteLine("hit ETNER to proceed...");
                            Console.ReadLine();

                            Console.WriteLine("After normal repair, the file still failed - trying NUCLEAR OPTION..");
                            Console.WriteLine("re-running repair with ForceRebuildXrefTable...  this is slow");

                            RepairOptions opts2 = new RepairOptions();
                            opts2.StructureOptions.ForceRebuildCrossReferenceTable = true;

                            opts2.ProblemEncountered += new EventHandler<Atalasoft.Shared.Validation.ProblemEventArgs>(opts_ProblemEncountered);
                            opts2.ProblemSkipped += new EventHandler<Atalasoft.Shared.Validation.ProblemEventArgs>(opts_ProblemSkipped);
                            opts2.ProblemRepaired += new EventHandler<Atalasoft.Shared.Validation.ProblemEventArgs>(opts_ProblemRepaired);

                            Console.WriteLine("Attempting rebuild... ");
                            PdfDocument rebuildDoc = new PdfDocument(inFile, userPass, ownerPass, opts2);
                            rebuildDoc.AllowSavingOfPreviouslySignedDocuments = true;
                            rebuildDoc.Save(inFile + ".REBUILD_XREF.11.5.pdf", new PdfSaveOptions() { PdfASavingBehavior = Atalasoft.PdfDoc.PdfA.PdfASavingBehavior.SavePdf });
                            rebuildDoc.Close();
                        }
                    }
                }
                finally
                {
                    if (File.Exists(inFile + ".testSave.11.5.pdf"))
                    {
                        //File.Delete(inFile + ".testSave.11.5.pdf");
                    }
                }


                Console.WriteLine("END Processing");
            }

            Console.WriteLine("PdfDocCheckAndRepairWinForm finished... press RETURN to exit");
            Console.ReadLine();
        }

        static void opts_ProblemRepaired(object sender, Atalasoft.Shared.Validation.ProblemEventArgs e)
        {
            Console.WriteLine("Problem Repaired: " + e.Problem.Name);
            PdfRepairConsequences cons = e.Problem.Consequences as PdfRepairConsequences;
            Console.WriteLine("Severity: " + cons.Description.ToString());
            //if (cons.Severity == Severity.Major || cons.Severity == Severity.Catastrophic || cons.Severity ==Severity.Serious) {
                Console.WriteLine("Severity: " + cons.Severity.ToString());
            //}
           
        }

        static void opts_ProblemSkipped(object sender, Atalasoft.Shared.Validation.ProblemEventArgs e)
        {
            Console.WriteLine("Problem Skipped: " + e.Problem.Name);
            PdfRepairConsequences cons = e.Problem.Consequences as PdfRepairConsequences;
            Console.WriteLine("Severity: " + cons.Severity.ToString());
            
        }

        static void opts_ProblemEncountered(object sender, Atalasoft.Shared.Validation.ProblemEventArgs e)
        {

            //Console.WriteLine("Problem Encoundered: " + e.Problem.Name);
            //Console.WriteLine("description: " + e.Problem.Description);
            //PdfRepairConsequences cons = e.Problem.Consequences as PdfRepairConsequences;
            //Console.WriteLine("Severity: " + cons.Severity.ToString());
            //Console.WriteLine("consequence: " + e.Problem.Consequences.Description);
           
        }

        /// <summary>
        /// Convenience method to get the root directory of the project - really only useful for debugging
        /// </summary>
        /// <returns></returns>
        private static string GetWorkingDir()
        {
            string cwd = System.IO.Directory.GetCurrentDirectory();
            //Console.WriteLine("cwd is '{0}'", cwd);

            if (cwd.EndsWith("\\bin\\Debug"))
            {
                cwd = cwd.Replace("\\bin\\Debug", "\\..\\..\\");
                //Console.WriteLine("updated cwd is '{0}'", cwd);
            }
            return cwd;
        }


        // SPECIAL ATTEMPT FOR INVALID CHAR \0 AT HEX STRING CODE
        private static bool FixInvalidCharAtHexString(string inFile, string outFile) {
            bool fixStatus = false;

            if (File.Exists(outFile))
            {
                File.Delete(outFile);
            }
            File.Copy(inFile, outFile);


            using (Stream fs = File.Open(outFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                //fs.Position = fs.Length - 200; //Skip to the end for the sake of brevity.

                byte[] trailer = Encoding.ASCII.GetBytes("trailer"); //byte[] "trailer"
                bool trailerFound = false;

                byte[] objStart = Encoding.ASCII.GetBytes("<<"); //byte[] "<<"
                bool objStartFound = false;

                byte[] objEnd = Encoding.ASCII.GetBytes(">>"); //byte[] ">>"
                byte currWordFound = 0;

                byte curr = 0;
                while (fs.Position < fs.Length)
                {
                    curr = (byte)fs.ReadByte();

                    if (!trailerFound)
                    {
                        if (curr == trailer[currWordFound])
                        currWordFound++;
                        else
                        currWordFound = 0;

                        if (currWordFound == trailer.Length)
                        {
                        trailerFound = true;
                        currWordFound = 0;
                        }

                        continue;
                    }

                    if(!objStartFound)
                    {
                        if (curr == objStart[currWordFound])
                        currWordFound++;
                        else
                        currWordFound = 0;

                        if (currWordFound == objStart.Length)
                        {
                        objStartFound = true;
                        currWordFound = 0;
                        }

                        continue;
                    }


                    //No conditional. Just always look for the trailer end tag and skip processing if we're outside the trailer.
                    {
                        if (curr == objEnd[currWordFound])
                        currWordFound++;
                        else
                        currWordFound = 0;

                        if (currWordFound == objEnd.Length) //If we've found the end, reset and start looking for the trailer again.
                        {
                            trailerFound = false;
                            objStartFound = false;
                            continue;
                        }
                    }

                    if (curr == '\0')
                    {
                        fs.Seek(-1, SeekOrigin.Current);
                        fs.WriteByte(48);
                    }
                }


                // REWIND so we can test the fix
                fs.Seek(0, SeekOrigin.Begin);
                Console.WriteLine("ATTEMPTED FIX as " + outFile);
                Console.WriteLine("Testing fixed document");
                try
                {
                    PdfDocument doc = new PdfDocument(fs);
                    doc.Close();
                    Console.WriteLine("\nFIX SUCCESSFUL");

                    fixStatus = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("FIX FAILED:");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine(ex.GetType().ToString());
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine("------------------------------------------");
                    fixStatus = false;
                }
                
            }
            return fixStatus;
        }

        // SPECIAL ATTEMPT FOR DOUBLE EOF
        public static void DoDoubleEOFStreamFix(string inFile, string outFile)
        {
            var stm = File.OpenRead(inFile);
            Stream fixedStm;
            TrimPdfStream(stm, out fixedStm);
            if (stm != fixedStm)
                stm.Dispose();
            using (fixedStm)
            {
                //var doc = new PdfDocument(null, null, fixedStm, null,
                //    new RepairOptions() {StructureOptions = {ForceRebuildCrossReferenceTable = true}});
                //var doc = new PdfDocument(null, null, fixedStm, null,
                //     new RepairOptions());
                PdfDocument doc = new PdfDocument(fixedStm, new RepairOptions());
                doc.Save(outFile);
                doc.Close();
            }
        }

        public static void TrimPdfStream(Stream inStm, out Stream outStm)
        {
            var tempFile = "fixedStream.pdf";
            outStm = inStm;
            outStm.Seek(EofLength, SeekOrigin.End); // skip last %%EOF
            var pos = GetLastEOFPosition(outStm);
            if (pos == -1)
            {
                // cannot repair
                if (outStm != inStm)
                {
                    outStm.Dispose();
                    File.Delete(tempFile);
                }
                outStm = null;
                return;
            }
            if (outStm == inStm)
            {
                var tmp = File.Create(tempFile);
                outStm.Seek(0, SeekOrigin.Begin);
                CopyStream(inStm, tmp, pos + EofLength);
                outStm = tmp;
            }
            else
                outStm.SetLength(pos + EofLength);
        }

        private const int EofLength = 5;

        public static void CopyStream(Stream input, Stream output, int bytes, int bufferSize = 2048)
        {
            var buffer = new byte[bufferSize];
            int read;
            while (bytes > 0 &&
                   (read = input.Read(buffer, 0, Math.Min(buffer.Length, bytes))) > 0)
            {
                output.Write(buffer, 0, read);
                bytes -= read;
            }
        }

        public static int GetLastEOFPosition(Stream stm, int bufferSize = 2048)
        {
            bufferSize = (int)Math.Min(stm.Length, bufferSize);
            var buf = new byte[bufferSize];
            stm.Seek(-bufferSize, SeekOrigin.Current);
            while (true)
            {
                stm.Read(buf, 0, bufferSize);
                var pos = GetLastEOFPosition(buf);
                if (pos != -1)
                    return (int)stm.Position - bufferSize + pos;

                if (stm.Position - 2 * bufferSize + EofLength - 1 < 0)
                    break;

                stm.Seek(-2 * bufferSize + EofLength - 1, SeekOrigin.Current);
            }
            bufferSize = (int)stm.Position - bufferSize + EofLength - 1;
            if (bufferSize < EofLength)
                return -1;
            stm.Seek(0, SeekOrigin.Current);
            stm.Read(buf, 0, bufferSize);
            return GetLastEOFPosition(buf);
        }

        public static int GetLastEOFPosition(byte[] arr)
        {
            for (int i = arr.Length - EofLength; i > 0; i--)
            {
                if (arr[i] == '%' &&
                    arr[i + 1] == '%' &&
                    arr[i + 2] == 'E' &&
                    arr[i + 3] == 'O' &&
                    arr[i + 4] == 'F')
                    return i;
            }
            return -1;
        }


    }
}
