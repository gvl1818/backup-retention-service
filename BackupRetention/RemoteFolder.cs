using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using AlexPilotti.FTPS.Client;
using AlexPilotti.FTPS.Common;

/*
Renci License:
New BSD License (BSD)
Copyright (c) 2010, RENCI
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

* Neither the name of RENCI nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

/*

AlexPilotti License:
 
GNU Library General Public License (LGPL)
Version 2.1, February 1999

Copyright (C) 1991, 1999 Free Software Foundation, Inc. 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA Everyone is permitted to copy and distribute verbatim copies of this license document, but changing it is not allowed.

[This is the first released version of the Lesser GPL. It also counts as the successor of the GNU Library Public License, version 2, hence the version number 2.1.]

Preamble

The licenses for most software are designed to take away your freedom to share and change it. By contrast, the GNU General Public Licenses are intended to guarantee your freedom to share and change free software--to make sure the software is free for all its users.

This license, the Lesser General Public License, applies to some specially designated software packages--typically libraries--of the Free Software Foundation and other authors who decide to use it. You can use it too, but we suggest you first think carefully about whether this license or the ordinary General Public License is the better strategy to use in any particular case, based on the explanations below.

When we speak of free software, we are referring to freedom of use, not price. Our General Public Licenses are designed to make sure that you have the freedom to distribute copies of free software (and charge for this service if you wish); that you receive source code or can get it if you want it; that you can change the software and use pieces of it in new free programs; and that you are informed that you can do these things.

To protect your rights, we need to make restrictions that forbid distributors to deny you these rights or to ask you to surrender these rights. These restrictions translate to certain responsibilities for you if you distribute copies of the library or if you modify it.

For example, if you distribute copies of the library, whether gratis or for a fee, you must give the recipients all the rights that we gave you. You must make sure that they, too, receive or can get the source code. If you link other code with the library, you must provide complete object files to the recipients, so that they can relink them with the library after making changes to the library and recompiling it. And you must show them these terms so they know their rights.

We protect your rights with a two-step method: (1) we copyright the library, and (2) we offer you this license, which gives you legal permission to copy, distribute and/or modify the library.

To protect each distributor, we want to make it very clear that there is no warranty for the free library. Also, if the library is modified by someone else and passed on, the recipients should know that what they have is not the original version, so that the original author's reputation will not be affected by problems that might be introduced by others.

Finally, software patents pose a constant threat to the existence of any free program. We wish to make sure that a company cannot effectively restrict the users of a free program by obtaining a restrictive license from a patent holder. Therefore, we insist that any patent license obtained for a version of the library must be consistent with the full freedom of use specified in this license.

Most GNU software, including some libraries, is covered by the ordinary GNU General Public License. This license, the GNU Lesser General Public License, applies to certain designated libraries, and is quite different from the ordinary General Public License. We use this license for certain libraries in order to permit linking those libraries into non-free programs.

When a program is linked with a library, whether statically or using a shared library, the combination of the two is legally speaking a combined work, a derivative of the original library. The ordinary General Public License therefore permits such linking only if the entire combination fits its criteria of freedom. The Lesser General Public License permits more lax criteria for linking other code with the library.

We call this license the "Lesser" General Public License because it does Less to protect the user's freedom than the ordinary General Public License. It also provides other free software developers Less of an advantage over competing non-free programs. These disadvantages are the reason we use the ordinary General Public License for many libraries. However, the Lesser license provides advantages in certain special circumstances.

For example, on rare occasions, there may be a special need to encourage the widest possible use of a certain library, so that it becomes a de-facto standard. To achieve this, non-free programs must be allowed to use the library. A more frequent case is that a free library does the same job as widely used non-free libraries. In this case, there is little to gain by limiting the free library to free software only, so we use the Lesser General Public License.

In other cases, permission to use a particular library in non-free programs enables a greater number of people to use a large body of free software. For example, permission to use the GNU C Library in non-free programs enables many more people to use the whole GNU operating system, as well as its variant, the GNU/Linux operating system.

Although the Lesser General Public License is Less protective of the users' freedom, it does ensure that the user of a program that is linked with the Library has the freedom and the wherewithal to run that program using a modified version of the Library.

The precise terms and conditions for copying, distribution and modification follow. Pay close attention to the difference between a "work based on the library" and a "work that uses the library". The former contains code derived from the library, whereas the latter must be combined with the library in order to run.

TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

0. This License Agreement applies to any software library or other program which contains a notice placed by the copyright holder or other authorized party saying it may be distributed under the terms of this Lesser General Public License (also called "this License"). Each licensee is addressed as "you".

A "library" means a collection of software functions and/or data prepared so as to be conveniently linked with application programs (which use some of those functions and data) to form executables.

The "Library", below, refers to any such software library or work which has been distributed under these terms. A "work based on the Library" means either the Library or any derivative work under copyright law: that is to say, a work containing the Library or a portion of it, either verbatim or with modifications and/or translated straightforwardly into another language. (Hereinafter, translation is included without limitation in the term "modification".)

"Source code" for a work means the preferred form of the work for making modifications to it. For a library, complete source code means all the source code for all modules it contains, plus any associated interface definition files, plus the scripts used to control compilation and installation of the library.

Activities other than copying, distribution and modification are not covered by this License; they are outside its scope. The act of running a program using the Library is not restricted, and output from such a program is covered only if its contents constitute a work based on the Library (independent of the use of the Library in a tool for writing it). Whether that is true depends on what the Library does and what the program that uses the Library does.

1. You may copy and distribute verbatim copies of the Library's complete source code as you receive it, in any medium, provided that you conspicuously and appropriately publish on each copy an appropriate copyright notice and disclaimer of warranty; keep intact all the notices that refer to this License and to the absence of any warranty; and distribute a copy of this License along with the Library.

You may charge a fee for the physical act of transferring a copy, and you may at your option offer warranty protection in exchange for a fee.

2. You may modify your copy or copies of the Library or any portion of it, thus forming a work based on the Library, and copy and distribute such modifications or work under the terms of Section 1 above, provided that you also meet all of these conditions:

a) The modified work must itself be a software library.

b) You must cause the files modified to carry prominent notices stating that you changed the files and the date of any change.

c) You must cause the whole of the work to be licensed at no charge to all third parties under the terms of this License.

d) If a facility in the modified Library refers to a function or a table of data to be supplied by an application program that uses the facility, other than as an argument passed when the facility is invoked, then you must make a good faith effort to ensure that, in the event an application does not supply such function or table, the facility still operates, and performs whatever part of its purpose remains meaningful.

(For example, a function in a library to compute square roots has a purpose that is entirely well-defined independent of the application. Therefore, Subsection 2d requires that any application-supplied function or table used by this function must be optional: if the application does not supply it, the square root function must still compute square roots.)

These requirements apply to the modified work as a whole. If identifiable sections of that work are not derived from the Library, and can be reasonably considered independent and separate works in themselves, then this License, and its terms, do not apply to those sections when you distribute them as separate works. But when you distribute the same sections as part of a whole which is a work based on the Library, the distribution of the whole must be on the terms of this License, whose permissions for other licensees extend to the entire whole, and thus to each and every part regardless of who wrote it.

Thus, it is not the intent of this section to claim rights or contest your rights to work written entirely by you; rather, the intent is to exercise the right to control the distribution of derivative or collective works based on the Library.

In addition, mere aggregation of another work not based on the Library with the Library (or with a work based on the Library) on a volume of a storage or distribution medium does not bring the other work under the scope of this License. 

3. You may opt to apply the terms of the ordinary GNU General Public License instead of this License to a given copy of the Library. To do this, you must alter all the notices that refer to this License, so that they refer to the ordinary GNU General Public License, version 2, instead of to this License. (If a newer version than version 2 of the ordinary GNU General Public License has appeared, then you can specify that version instead if you wish.) Do not make any other change in these notices.

Once this change is made in a given copy, it is irreversible for that copy, so the ordinary GNU General Public License applies to all subsequent copies and derivative works made from that copy.

This option is useful when you wish to copy part of the code of the Library into a program that is not a library.

4. You may copy and distribute the Library (or a portion or derivative of it, under Section 2) in object code or executable form under the terms of Sections 1 and 2 above provided that you accompany it with the complete corresponding machine-readable source code, which must be distributed under the terms of Sections 1 and 2 above on a medium customarily used for software interchange.

If distribution of object code is made by offering access to copy from a designated place, then offering equivalent access to copy the source code from the same place satisfies the requirement to distribute the source code, even though third parties are not compelled to copy the source along with the object code.

5. A program that contains no derivative of any portion of the Library, but is designed to work with the Library by being compiled or linked with it, is called a "work that uses the Library". Such a work, in isolation, is not a derivative work of the Library, and therefore falls outside the scope of this License.

However, linking a "work that uses the Library" with the Library creates an executable that is a derivative of the Library (because it contains portions of the Library), rather than a "work that uses the library". The executable is therefore covered by this License. Section 6 states terms for distribution of such executables.

When a "work that uses the Library" uses material from a header file that is part of the Library, the object code for the work may be a derivative work of the Library even though the source code is not. Whether this is true is especially significant if the work can be linked without the Library, or if the work is itself a library. The threshold for this to be true is not precisely defined by law.

If such an object file uses only numerical parameters, data structure layouts and accessors, and small macros and small inline functions (ten lines or less in length), then the use of the object file is unrestricted, regardless of whether it is legally a derivative work. (Executables containing this object code plus portions of the Library will still fall under Section 6.)

Otherwise, if the work is a derivative of the Library, you may distribute the object code for the work under the terms of Section 6. Any executables containing that work also fall under Section 6, whether or not they are linked directly with the Library itself.

6. As an exception to the Sections above, you may also combine or link a "work that uses the Library" with the Library to produce a work containing portions of the Library, and distribute that work under terms of your choice, provided that the terms permit modification of the work for the customer's own use and reverse engineering for debugging such modifications.

You must give prominent notice with each copy of the work that the Library is used in it and that the Library and its use are covered by this License. You must supply a copy of this License. If the work during execution displays copyright notices, you must include the copyright notice for the Library among them, as well as a reference directing the user to the copy of this License. Also, you must do one of these things:

a) Accompany the work with the complete corresponding machine-readable source code for the Library including whatever changes were used in the work (which must be distributed under Sections 1 and 2 above); and, if the work is an executable linked with the Library, with the complete machine-readable "work that uses the Library", as object code and/or source code, so that the user can modify the Library and then relink to produce a modified executable containing the modified Library. (It is understood that the user who changes the contents of definitions files in the Library will not necessarily be able to recompile the application to use the modified definitions.)

b) Use a suitable shared library mechanism for linking with the Library. A suitable mechanism is one that (1) uses at run time a copy of the library already present on the user's computer system, rather than copying library functions into the executable, and (2) will operate properly with a modified version of the library, if the user installs one, as long as the modified version is interface-compatible with the version that the work was made with.

c) Accompany the work with a written offer, valid for at least three years, to give the same user the materials specified in Subsection 6a, above, for a charge no more than the cost of performing this distribution.

d) If distribution of the work is made by offering access to copy from a designated place, offer equivalent access to copy the above specified materials from the same place.

e) Verify that the user has already received a copy of these materials or that you have already sent this user a copy.

For an executable, the required form of the "work that uses the Library" must include any data and utility programs needed for reproducing the executable from it. However, as a special exception, the materials to be distributed need not include anything that is normally distributed (in either source or binary form) with the major components (compiler, kernel, and so on) of the operating system on which the executable runs, unless that component itself accompanies the executable.

It may happen that this requirement contradicts the license restrictions of other proprietary libraries that do not normally accompany the operating system. Such a contradiction means you cannot use both them and the Library together in an executable that you distribute.

7. You may place library facilities that are a work based on the Library side-by-side in a single library together with other library facilities not covered by this License, and distribute such a combined library, provided that the separate distribution of the work based on the Library and of the other library facilities is otherwise permitted, and provided that you do these two things:

a) Accompany the combined library with a copy of the same work based on the Library, uncombined with any other library facilities. This must be distributed under the terms of the Sections above.

b) Give prominent notice with the combined library of the fact that part of it is a work based on the Library, and explaining where to find the accompanying uncombined form of the same work.

8. You may not copy, modify, sublicense, link with, or distribute the Library except as expressly provided under this License. Any attempt otherwise to copy, modify, sublicense, link with, or distribute the Library is void, and will automatically terminate your rights under this License. However, parties who have received copies, or rights, from you under this License will not have their licenses terminated so long as such parties remain in full compliance.

9. You are not required to accept this License, since you have not signed it. However, nothing else grants you permission to modify or distribute the Library or its derivative works. These actions are prohibited by law if you do not accept this License. Therefore, by modifying or distributing the Library (or any work based on the Library), you indicate your acceptance of this License to do so, and all its terms and conditions for copying, distributing or modifying the Library or works based on it.

10. Each time you redistribute the Library (or any work based on the Library), the recipient automatically receives a license from the original licensor to copy, distribute, link with or modify the Library subject to these terms and conditions. You may not impose any further restrictions on the recipients' exercise of the rights granted herein. You are not responsible for enforcing compliance by third parties with this License.

11. If, as a consequence of a court judgment or allegation of patent infringement or for any other reason (not limited to patent issues), conditions are imposed on you (whether by court order, agreement or otherwise) that contradict the conditions of this License, they do not excuse you from the conditions of this License. If you cannot distribute so as to satisfy simultaneously your obligations under this License and any other pertinent obligations, then as a consequence you may not distribute the Library at all. For example, if a patent license would not permit royalty-free redistribution of the Library by all those who receive copies directly or indirectly through you, then the only way you could satisfy both it and this License would be to refrain entirely from distribution of the Library.

If any portion of this section is held invalid or unenforceable under any particular circumstance, the balance of the section is intended to apply, and the section as a whole is intended to apply in other circumstances.

It is not the purpose of this section to induce you to infringe any patents or other property right claims or to contest validity of any such claims; this section has the sole purpose of protecting the integrity of the free software distribution system which is implemented by public license practices. Many people have made generous contributions to the wide range of software distributed through that system in reliance on consistent application of that system; it is up to the author/donor to decide if he or she is willing to distribute software through any other system and a licensee cannot impose that choice.

This section is intended to make thoroughly clear what is believed to be a consequence of the rest of this License.

12. If the distribution and/or use of the Library is restricted in certain countries either by patents or by copyrighted interfaces, the original copyright holder who places the Library under this License may add an explicit geographical distribution limitation excluding those countries, so that distribution is permitted only in or among countries not thus excluded. In such case, this License incorporates the limitation as if written in the body of this License.

13. The Free Software Foundation may publish revised and/or new versions of the Lesser General Public License from time to time. Such new versions will be similar in spirit to the present version, but may differ in detail to address new problems or concerns.

Each version is given a distinguishing version number. If the Library specifies a version number of this License which applies to it and "any later version", you have the option of following the terms and conditions either of that version or of any later version published by the Free Software Foundation. If the Library does not specify a license version number, you may choose any version ever published by the Free Software Foundation.

14. If you wish to incorporate parts of the Library into other free programs whose distribution conditions are incompatible with these, write to the author to ask for permission. For software which is copyrighted by the Free Software Foundation, write to the Free Software Foundation; we sometimes make exceptions for this. Our decision will be guided by the two goals of preserving the free status of all derivatives of our free software and of promoting the sharing and reuse of software generally.

NO WARRANTY

15. BECAUSE THE LIBRARY IS LICENSED FREE OF CHARGE, THERE IS NO WARRANTY FOR THE LIBRARY, TO THE EXTENT PERMITTED BY APPLICABLE LAW. EXCEPT WHEN OTHERWISE STATED IN WRITING THE COPYRIGHT HOLDERS AND/OR OTHER PARTIES PROVIDE THE LIBRARY "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE LIBRARY IS WITH YOU. SHOULD THE LIBRARY PROVE DEFECTIVE, YOU ASSUME THE COST OF ALL NECESSARY SERVICING, REPAIR OR CORRECTION.

16. IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING WILL ANY COPYRIGHT HOLDER, OR ANY OTHER PARTY WHO MAY MODIFY AND/OR REDISTRIBUTE THE LIBRARY AS PERMITTED ABOVE, BE LIABLE TO YOU FOR DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE THE LIBRARY (INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING RENDERED INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A FAILURE OF THE LIBRARY TO OPERATE WITH ANY OTHER SOFTWARE), EVEN IF SUCH HOLDER OR OTHER PARTY HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. 
  
 */
namespace BackupRetention
{
    /// <summary>
    /// Remote Folder Class will upload or download an entire directory to/from SFTP,FTPS, or FTP
    /// </summary>
    public class RemoteFolder: IFolderConfig
    {
        #region "Variables"
        /// <summary>
        /// List of all Files to Upload 
        /// </summary>
        private System.Collections.Generic.List<System.IO.FileInfo> UploadFiles=null;

        /// <summary>
        /// List of all Key Files
        /// </summary>
        private System.Collections.Generic.List<System.IO.FileInfo> KeyFiles=null;

        /// <summary>
        /// List of all Files that were uploaded
        /// </summary>
        private System.Collections.Generic.List<System.IO.FileInfo> FilesUploaded=null;

        /// <summary>
        /// List of all Files that were downloaded
        /// </summary>
        private System.Collections.Generic.List<System.IO.FileInfo> FilesDownloaded=null;

        private System.Diagnostics.EventLog _evt;
        private string ep = "6315270D-F7BD-4734-81EC-312A48D01436";
        #endregion

        #region "Properties"
        private int _id = 1;
        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }

        }

        private string _title = "";
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }

        }

        private string _time = "";
        /// <summary>
        /// Time to execute upload or download of entire directory
        /// </summary>
        public string Time
        {
            get
            {
                return _time;
            }

            set
            {
                _time = value;
            }

        }

        private string _endTime = "";
        public string EndTime
        {
            get
            {
                return _endTime;
            }

            set
            {
                _endTime = value;
            }

        }

        private IntervalTypes _intervalType = IntervalTypes.Daily;
        public IntervalTypes IntervalType
        {
            get
            {
                return _intervalType;
            }

            set
            {
                _intervalType = value;
            }

        }

        private long _interval = 0;
        public long Interval
        {
            get
            {
                return _interval;
            }

            set
            {
                _interval = value;
            }

        }

        private bool _monday = false;
        public bool Monday
        {
            get
            {
                return _monday;
            }
            set
            {
                _monday = value;
            }
        }

        private bool _tuesday = false;
        public bool Tuesday
        {
            get
            {
                return _tuesday;
            }
            set
            {
                _tuesday = value;
            }
        }

        private bool _wednesday = false;
        public bool Wednesday
        {
            get
            {
                return _wednesday;
            }
            set
            {
                _wednesday = value;
            }
        }

        private bool _thursday = false;
        public bool Thursday
        {
            get
            {
                return _thursday;
            }
            set
            {
                _thursday = value;
            }
        }

        private bool _friday = false;
        public bool Friday
        {
            get
            {
                return _friday;
            }
            set
            {
                _friday = value;
            }
        }

        private bool _saturday = false;
        public bool Saturday
        {
            get
            {
                return _saturday;
            }
            set
            {
                _saturday = value;
            }
        }

        private bool _sunday = false;
        public bool Sunday
        {
            get
            {
                return _sunday;
            }
            set
            {
                _sunday = value;
            }
        }

        private Month _months = Month.January | Month.February | Month.March | Month.April | Month.May | Month.June | Month.July | Month.August | Month.September | Month.October | Month.November | Month.December;
        public Month Months
        {
            get
            {
                return _months;
            }

            set
            {
                _months = value;
            }
        }

        private bool _enabled = true;
        /// <summary>
        /// Remote Upload or Download of entire directory enabled?
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }


        }

        private string _comment = "";
        public string Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                _comment = value;
            }

        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }

            set
            {
                _startDate = value;
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }

            set
            {
                _endDate = value;
            }
        }

        private string _host = "";
        /// <summary>
        /// Remote Server IP address or hostname
        /// </summary>
        public string Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }

        private int _port = 22;
        /// <summary>
        /// Port for remote protocol
        /// </summary>
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        private ProtocolOptions _protocol = ProtocolOptions.SFTP;
        /// <summary>
        /// Remote Server protocol
        /// </summary>
        public ProtocolOptions Protocol
        {
            get
            {
                return _protocol;
            }
            set
            {
                _protocol = value;
            }
        }

        private bool _allowAnyCertificate = true;
        /// <summary>
        /// FTPS Allow Any Certificate or only valid
        /// </summary>
        public bool AllowAnyCertificate
        {
            get
            {
                return _allowAnyCertificate;
            }
            set
            {
                _allowAnyCertificate = value;
            }
        }

        private int _timeout = 120000;
        /// <summary>
        /// FTPS timeout
        /// </summary>
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }


        private string _username = "";
        /// <summary>
        /// Username for remote connection
        /// </summary>
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        private string _keyFileDirectory = "";
        /// <summary>
        /// Directory path for keyfiles
        /// </summary>
        public string KeyFileDirectory
        {
            get
            {
                return _keyFileDirectory;
            }
            set
            {
                _keyFileDirectory = Common.WindowsPathClean(value);
            }
        }

        private bool _usePassPhrase = false;
        /// <summary>
        /// Use Password for Key file
        /// </summary>
        public bool UsePassPhrase
        {
            get
            {
                return _usePassPhrase;
            }
            set
            {
                _usePassPhrase = value;
            }
        }

        private string _password = "";
        /// <summary>
        /// Password for remote server connection or password for keyfile
        /// </summary>
        public string Password
        {
            set
            {
                _password = value;
            }
        }


        private string _remoteDirectory = "";
        /// <summary>
        /// Remote folder to upload to or download from
        /// </summary>
        public string RemoteDirectory
        {
            get
            {
                return _remoteDirectory;
            }
            set
            {
                _remoteDirectory = value;
            }
        }

        private string _backupFolder = "";
        /// <summary>
        /// Local Folder to upload or download to
        /// </summary>
        public string BackupFolder
        {
            get
            {
                return _backupFolder;
            }
            set
            {
                _backupFolder = Common.WindowsPathClean(value);
            }
        }

        private TransferDirectionOptions _transferDirection = TransferDirectionOptions.Upload;
        /// <summary>
        /// Upload or Download entire directory
        /// </summary>
        public TransferDirectionOptions TransferDirection
        {
            get
            {
                return _transferDirection;
            }
            set
            {
                _transferDirection = value;
            }
        }

        private OverwriteOptions _overwrite = OverwriteOptions.FileSizeChangeOverwrite;
        /// <summary>
        /// Whether to Overwrite files, not overwrite, or overwrite based on last modified.
        /// </summary>
        public OverwriteOptions Overwrite
        {
            get
            {
                return _overwrite;
            }
            set
            {
                _overwrite = value;
            }
        }

        public static string _fileNameFilter = "";
        public string FileNameFilter
        {
            get
            {
                return _fileNameFilter;
            }
            set
            {
                _fileNameFilter = value;
            }
        }
        #endregion
        #region "Methods"

        /// <summary>
        /// Initializes RemoteConfig datatable and returns it
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable init_dtConfig()
        {
            DataTable dtRemote;
            dtRemote = new DataTable("RemoteConfig");

            //Create Primary Key Column
            DataColumn dcID = new DataColumn("ID", typeof(Int32));
            dcID.AllowDBNull = false;
            dcID.Unique = true;
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 1;
            dcID.AutoIncrementStep = 1;

            //Assign Primary Key
            DataColumn[] columns = new DataColumn[1];
            dtRemote.Columns.Add(dcID);
            columns[0] = dtRemote.Columns["ID"];
            dtRemote.PrimaryKey = columns;


            //Create Columns
            dtRemote.Columns.Add(new DataColumn("Enabled", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Title", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Time", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("EndTime", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("IntervalType", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Interval", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Monday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Tuesday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Wednesday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Thursday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Friday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Saturday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Sunday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("January", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("February", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("March", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("April", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("May", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("June", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("July", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("August", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("September", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("October", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("November", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("December", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Host", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Protocol", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Port", typeof(Int32)));
            dtRemote.Columns.Add(new DataColumn("Username", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Password", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("KeyFileDirectory", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("UsePassPhrase", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("BackupFolder", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("RemoteDirectory", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("TransferDirection", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Overwrite", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("AllowAnyCertificate", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Timeout", typeof(Int32)));
            dtRemote.Columns.Add(new DataColumn("FileNameFilter", typeof(string)));
            dtRemote.Columns.Add(new DataColumn("StartDate", typeof(string)));
            dtRemote.Columns.Add(new DataColumn("EndDate", typeof(string)));
            dtRemote.Columns.Add(new DataColumn("Comment", typeof(string)));
            dtRemote.Columns["Enabled"].DefaultValue = "true";
            dtRemote.Columns["Time"].DefaultValue = "01:00";
            dtRemote.Columns["IntervalType"].DefaultValue = "Daily";
            dtRemote.Columns["Interval"].DefaultValue = "0";
            dtRemote.Columns["Monday"].DefaultValue = "true";
            dtRemote.Columns["Tuesday"].DefaultValue = "true";
            dtRemote.Columns["Wednesday"].DefaultValue = "true";
            dtRemote.Columns["Thursday"].DefaultValue = "true";
            dtRemote.Columns["Friday"].DefaultValue = "true";
            dtRemote.Columns["Saturday"].DefaultValue = "true";
            dtRemote.Columns["Sunday"].DefaultValue = "true";
            dtRemote.Columns["January"].DefaultValue = "true";
            dtRemote.Columns["February"].DefaultValue = "true";
            dtRemote.Columns["March"].DefaultValue = "true";
            dtRemote.Columns["April"].DefaultValue = "true";
            dtRemote.Columns["May"].DefaultValue = "true";
            dtRemote.Columns["June"].DefaultValue = "true";
            dtRemote.Columns["July"].DefaultValue = "true";
            dtRemote.Columns["August"].DefaultValue = "true";
            dtRemote.Columns["September"].DefaultValue = "true";
            dtRemote.Columns["October"].DefaultValue = "true";
            dtRemote.Columns["November"].DefaultValue = "true";
            dtRemote.Columns["December"].DefaultValue = "true";

            dtRemote.Columns["Time"].DefaultValue = "00:00";
            dtRemote.Columns["Protocol"].DefaultValue = "SFTP";
            dtRemote.Columns["TransferDirection"].DefaultValue = "Upload";
            dtRemote.Columns["Overwrite"].DefaultValue = "ForceOverwrite";
            dtRemote.Columns["Port"].DefaultValue = 22;
            dtRemote.Columns["Timeout"].DefaultValue = 120000;
            dtRemote.Columns["AllowAnyCertificate"].DefaultValue = "true";

            return dtRemote;
        }

        private void init_RemoteFolder()
        {
            _evt = Common.GetEventLog;
        }

        /// <summary>
        /// Default contructor
        /// </summary>
        public RemoteFolder()
        {
            init_RemoteFolder();

        }

        /// <summary>
        /// Remote Folder Contructor for when passed a datarow
        /// </summary>
        /// <param name="row"></param>
        public RemoteFolder(DataRow row)
        {
            init_RemoteFolder();

            string str = "";
            ID = Common.FixNullInt32(row["ID"]);
            Enabled = Common.FixNullbool(row["Enabled"]);
            Time = Common.FixNullstring(row["Time"]);
            EndTime = Common.FixNullstring(row["EndTime"]);
            try
            {
                IntervalType = (IntervalTypes)System.Enum.Parse(typeof(IntervalTypes), Common.FixNullstring(row["IntervalType"]));
            }
            catch (Exception)
            {
                IntervalType = IntervalTypes.Daily;
            }
            Interval = Common.FixNulllong(row["Interval"]);
            Monday = Common.FixNullbool(row["Monday"]);
            Tuesday = Common.FixNullbool(row["Tuesday"]);
            Wednesday = Common.FixNullbool(row["Wednesday"]);
            Thursday = Common.FixNullbool(row["Thursday"]);
            Friday = Common.FixNullbool(row["Friday"]);
            Saturday = Common.FixNullbool(row["Saturday"]);
            Sunday = Common.FixNullbool(row["Sunday"]);

            Months = 0;
            if (Common.FixNullbool(row["January"]))
            {
                Months = Months | Month.January;
            }
            if (Common.FixNullbool(row["February"]))
            {
                Months = Months | Month.February;
            }
            if (Common.FixNullbool(row["March"]))
            {
                Months = Months | Month.March;
            }
            if (Common.FixNullbool(row["April"]))
            {
                Months = Months | Month.April;
            }
            if (Common.FixNullbool(row["May"]))
            {
                Months = Months | Month.May;
            }
            if (Common.FixNullbool(row["June"]))
            {
                Months = Months | Month.June;
            }
            if (Common.FixNullbool(row["July"]))
            {
                Months = Months | Month.July;
            }
            if (Common.FixNullbool(row["August"]))
            {
                Months = Months | Month.August;
            }
            if (Common.FixNullbool(row["September"]))
            {
                Months = Months | Month.September;
            }
            if (Common.FixNullbool(row["October"]))
            {
                Months = Months | Month.October;
            }
            if (Common.FixNullbool(row["November"]))
            {
                Months = Months | Month.November;
            }
            if (Common.FixNullbool(row["December"]))
            {
                Months = Months | Month.December;
            }
            DateTime.TryParse(Common.FixNullstring(row["StartDate"]), out _startDate);
            DateTime.TryParse(Common.FixNullstring(row["EndDate"]), out _endDate);

            Host = Common.FixNullstring(row["Host"]);
            str = Common.FixNullstring(row["Protocol"]);
            try
            {
                Protocol = (ProtocolOptions)System.Enum.Parse(typeof(ProtocolOptions), str);
            }
            catch (Exception)
            {
                Protocol = ProtocolOptions.SFTP;
            }
            Port = Common.FixNullInt32(row["Port"]);
            Username = Common.FixNullstring(row["Username"]);
            Password = Common.FixNullstring(row["Password"]);
            KeyFileDirectory = Common.FixNullstring(row["KeyFileDirectory"]);
            UsePassPhrase = Common.FixNullbool(row["UsePassPhrase"]);
            BackupFolder = Common.FixNullstring(row["BackupFolder"]);
            RemoteDirectory = Common.FixNullstring(row["RemoteDirectory"]);
            AllowAnyCertificate = Common.FixNullbool(row["AllowAnyCertificate"]);
            Timeout = Common.FixNullInt32(row["Timeout"]);

            str = Common.FixNullstring(row["TransferDirection"]);
            try
            {
                TransferDirection = (TransferDirectionOptions)System.Enum.Parse(typeof(TransferDirectionOptions), str);
            }
            catch (Exception)
            {
                TransferDirection = TransferDirectionOptions.Upload;
            }

            str = Common.FixNullstring(row["Overwrite"]);
            try
            {
                Overwrite = (OverwriteOptions)System.Enum.Parse(typeof(OverwriteOptions), str);
            }
            catch (Exception)
            {
                Overwrite = OverwriteOptions.ForceOverwrite;
            }
            FileNameFilter = Common.FixNullstring(row["FileNameFilter"]);

            Title = Common.FixNullstring(row["Title"]);
            Comment = Common.FixNullstring(row["Comment"]);
        }

        /// <summary>
        /// Remote Folder Deconstructor
        /// </summary>
        ~RemoteFolder()
        {
            try
            {
                if (UploadFiles != null)
                {
                    UploadFiles.Clear();
                }
                if (FilesUploaded != null)
                {
                    FilesUploaded.Clear();
                }
                if (FilesDownloaded != null)
                {
                    FilesDownloaded.Clear();
                }
            }
            catch (Exception)
            {

            }

            UploadFiles = null;
            FilesUploaded = null;
            FilesDownloaded = null;
            _evt = null;
        }

        /// <summary>
        /// Executes Remote Configured Upload or Download of files
        /// </summary>
        /// <param name="blShuttingDown"></param>
        public void Execute(ref bool blShuttingDown)
        {
            if (this.Enabled)
            {
                switch (Protocol)
                {
                    case ProtocolOptions.SFTP:
                        SFTP(ref blShuttingDown);
                        break;
                    case ProtocolOptions.FTPsExplicit:
                        FTPs(ref blShuttingDown);
                        break;
                    case ProtocolOptions.FTPsImplicit:
                        FTPs(ref blShuttingDown);
                        break;
                    case ProtocolOptions.FTP:
                        FTPs(ref blShuttingDown);
                        break;
                }
            }
        }



        /// <summary>
        /// SFTP procedure to upload or download files
        /// </summary>
        /// <param name="blShuttingDown"></param>
        public void SFTP(ref bool blShuttingDown)
        {
            SftpClient sftp = null;
            try
            {
                string upassword = "";
                AES256 aes = new AES256(ep);
                if (_password.Length > 0)
                {
                    upassword = aes.Decrypt(_password);
                }
                if (string.IsNullOrEmpty(KeyFileDirectory))
                {
                    sftp = new SftpClient(Host, Port, Username, upassword);
                }
                else
                {
                    KeyFiles = Common.WalkDirectory(KeyFileDirectory, ref blShuttingDown);
                    List<PrivateKeyFile> PKeyFiles = new List<PrivateKeyFile>();
                    foreach (FileInfo kfile in KeyFiles)
                    {
                        PrivateKeyFile p;
                        if (UsePassPhrase)
                        {
                            p = new PrivateKeyFile(kfile.FullName, upassword);
                        }
                        else
                        {
                            p = new PrivateKeyFile(kfile.FullName);
                        }
                        PKeyFiles.Add(p);
                    }


                    sftp = new SftpClient(Host, Port, Username, PKeyFiles.ToArray());
                }
                
                BackupFolder = BackupFolder.Replace("\\\\", "\\");
                UploadFiles = Common.WalkDirectory(BackupFolder, ref blShuttingDown, FileNameFilter);
                sftp.Connect();
                upassword = "";
                _evt.WriteEntry("Remote Sync SFTP: Connected successfully to Host: " + Host, System.Diagnostics.EventLogEntryType.Information,1005, 10);
                                        
                switch (TransferDirection)
                {
                    case TransferDirectionOptions.Upload:
                        List<DirectoryInfo> Dirs = null;
                        Stream ureader = null;
                        try
                        {
                            string strRemotePath = "";
                            strRemotePath = Common.FixNullstring(RemoteDirectory);
                            if (RemoteDirectory != "")
                            {
                                strRemotePath = RemoteDirectory + "/";
                            }
                            strRemotePath = Common.RemotePathClean(Common.FixNullstring(strRemotePath));
                            BackupFolder = Common.WindowsPathClean(Common.FixNullstring(BackupFolder));

                            if (!string.IsNullOrEmpty(strRemotePath))
                            {
                                if (!sftp.Exists(strRemotePath))
                                {
                                    sftp.CreateDirectory(strRemotePath);
                                }
                                sftp.ChangeDirectory(strRemotePath);
                            }

                            //Create Directories
                            Dirs = Common.GetAllDirectories(BackupFolder);
                            foreach (DirectoryInfo dir in Dirs)
                            {
                                if (blShuttingDown)
                                {
                                    throw new Exception("Shutting Down");
                                }
                                try
                                {
                                    strRemotePath = dir.FullName;
                                    strRemotePath = Common.RemotePathCombine(RemoteDirectory, strRemotePath, BackupFolder);
                                    if (blShuttingDown)
                                    {
                                        _evt.WriteEntry("Remote Sync: Shutting Down, about to Create a Folder: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 1040, 10);
                                        return;
                                    }
                                }
                                catch (Exception)
                                {
                                    _evt.WriteEntry("Remote Sync: Folder Path Text Formatting Error: " + strRemotePath, System.Diagnostics.EventLogEntryType.Error, 1040, 10);
                                }
                                try
                                {

                                    sftp.CreateDirectory(strRemotePath);
                                    _evt.WriteEntry("Remote Sync: Folder Created on " + Host + ": " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 1040, 10);

                                }
                                catch (Exception)
                                {

                                }
                            }

                            //Upload every file found and place in the correct directory
                            UploadFiles = Common.WalkDirectory(BackupFolder, ref blShuttingDown, FileNameFilter);
                            foreach (FileInfo ufile in UploadFiles)
                            {

                                try
                                {
                                    bool blFileFound = false;
                                    bool blOverwriteFile = false;

                                    if (blShuttingDown)
                                    {
                                        throw new Exception("Shutting Down");
                                    }

                                    strRemotePath = ufile.DirectoryName;
                                    strRemotePath = Common.RemotePathCombine(Common.FixNullstring(RemoteDirectory),strRemotePath,Common.FixNullstring(BackupFolder));
                                    
                                    

                                    if (!string.IsNullOrEmpty(strRemotePath))
                                    {
                                        sftp.ChangeDirectory(strRemotePath);
                                    }
                                    
                                    strRemotePath = Common.RemotePathCombine(strRemotePath, ufile.Name);


                                    if (sftp.Exists(strRemotePath))
                                    {
                                        blFileFound = true;
                                        //if (!((ufile.LastWriteTimeUtc == sftp.GetLastWriteTimeUtc(strRemotePath))))
                                        SftpFile mupload = sftp.Get(strRemotePath);
                                        if (ufile.Length != mupload.Length)
                                        {
                                            blOverwriteFile = true;
                                        }
                                    }

                                    if (sftp.Exists(strRemotePath + ".7z"))
                                    {
                                        blFileFound = true;
                                        SftpFile mupload = sftp.Get(strRemotePath + ".7z");
                                        
                                        //File Size can't be used to compare and last write time cannot be modified so can't compare
                                        blOverwriteFile = true;  //Overwrite Options can be specified by the user

                                        /*if (!((ufile.LastWriteTimeUtc == mupload.LastWriteTimeUtc)))
                                        {
                                            blOverwriteFile = true;
                                        }*/
                                    }
                                    if (blShuttingDown)
                                    {

                                        _evt.WriteEntry("Remote Sync: Shutting Down, about to possibly upload a file: " + ufile.FullName + " Host: " + Host + " To: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 1010, 10);
                                        return;
                                    }
                                    
                                    if ((blFileFound == false || blOverwriteFile || (Overwrite == OverwriteOptions.ForceOverwrite && blFileFound)) && !(Overwrite == OverwriteOptions.NoOverwrite && blFileFound == true))
                                    {
                                        /*if (sftp.Exists(ufile.FullName))
                                        {
                                            SftpFileAttributes fa=sftp.GetAttributes(ufile.FullName);
                                            fa.OwnerCanWrite = true;
                                            fa.OthersCanWrite = true;
                                            fa.GroupCanWrite = true;
                                            sftp.SetAttributes(ufile.FullName,fa);
                                            sftp.DeleteFile(ufile.FullName);
                                        }*/
                                            
                                        ureader = new FileStream(ufile.FullName, FileMode.Open);
                                        //sftp.UploadFile(File.OpenRead(ufile.FullName), strRemotePath);
                                        sftp.UploadFile(ureader, strRemotePath);
                                            
                                        _evt.WriteEntry("Remote Sync SFTP: File Uploaded: " + ufile.FullName + " Host: " + Host + " To: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 1010, 10);
                                        ureader.Close();
                                        ureader = null;
                                        //sftp.SetLastWriteTime(ufile.Name, ufile.LastWriteTimeUtc); //not implemented yet 
                                    }
                                    

                                }
                                catch (Exception ex)
                                {
                                    _evt.WriteEntry("Remote Sync SFTP: Host: " + Host + "Upload of FileName: " + ufile.FullName + " Error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error, 1000, 10);
                                }

                            }
                        }
                        catch (Exception exsftup)
                        {
                            _evt.WriteEntry("Remote Sync SFTP: Host: " + Host + " Error: " + exsftup.Message, System.Diagnostics.EventLogEntryType.Error, 1000, 10);
                                
                        }
                        finally
                        {
                            if (Dirs != null)
                            {
                                Dirs.Clear();
                            }
                            Dirs = null;
                            if (ureader != null)
                            {
                                ureader.Close();
                                ureader = null;
                            }
                            if (UploadFiles != null)
                            {
                                UploadFiles.Clear();
                            }
                            UploadFiles = null;
                        }
                       
                        break;
                    case TransferDirectionOptions.Download:
                        
                        List<RemoteFile> RemoteFiles=null;
                        try
                        {
                            if (!string.IsNullOrEmpty(RemoteDirectory))
                            {
                                sftp.ChangeDirectory(RemoteDirectory);

                            }

                            RemoteFiles = Common.GetRemoteDirectories(RemoteDirectory, sftp, "");
                            //Creates Folder Locally in the BackupFolder
                            Common.CreateLocalFolderStructure(RemoteFiles, BackupFolder);

                            //Downloads Files in each directory
                            foreach (RemoteFile ftpfile in RemoteFiles)
                            {
                                string DownloadPath = "";
                                string strLocalFilePath = "";
                                string strRemoteFilePath = "";
                                if (blShuttingDown)
                                {
                                    throw new Exception("Shutting Down");
                                }
                                try
                                {
                                    DownloadPath = Common.RemotePathCombine(RemoteDirectory,ftpfile.ParentDirectory);
                                    if (!ftpfile.IsDirectory)
                                    {
                                        strLocalFilePath = Common.WindowsPathCombine(BackupFolder, ftpfile.ParentDirectory);
                                        strLocalFilePath = Common.WindowsPathCombine(strLocalFilePath, ftpfile.Name);
                                        strRemoteFilePath = Common.RemotePathCombine(DownloadPath, ftpfile.Name);

                                        if (blShuttingDown)
                                        {
                                            if (sftp != null)
                                            {
                                                if (sftp.IsConnected)
                                                {
                                                    sftp.Disconnect();
                                                }
                                                sftp.Dispose();
                                            }
                                            _evt.WriteEntry("Remote Sync: Shutting Down, about to possibly download a Host: " + Host + " file: " + DownloadPath + "/" + ftpfile.Name + " From: " + strLocalFilePath, System.Diagnostics.EventLogEntryType.Information, 1020, 10);
                                            return;
                                        }
                                        
                                        if (Common.DownloadFile(strLocalFilePath, strRemoteFilePath, ftpfile, Overwrite))
                                        {

                                            if (Common.FixNullstring(FileNameFilter) != "" && Common.VerifyPattern(FileNameFilter))
                                            {
                                                if (Common.FileNameMatchesPattern(FileNameFilter, ftpfile.Name))
                                                {
                                                    if (File.Exists(strLocalFilePath))
                                                    {
                                                        File.SetAttributes(strLocalFilePath, FileAttributes.Normal);
                                                        File.Delete(strLocalFilePath);
                                                    }
                                                    using (FileStream fs = new FileStream(strLocalFilePath, FileMode.Create))
                                                    {

                                                        sftp.DownloadFile(strRemoteFilePath, fs);
                                                        _evt.WriteEntry("Remote Sync SFTP: File Downloaded: " + strRemoteFilePath + " Host: " + Host + " To: " + strLocalFilePath, System.Diagnostics.EventLogEntryType.Information, 1020, 10);
                                                        fs.Close();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (File.Exists(strLocalFilePath))
                                                {
                                                    File.SetAttributes(strLocalFilePath, FileAttributes.Normal);
                                                    File.Delete(strLocalFilePath);
                                                }
                                                using (FileStream fs = new FileStream(strLocalFilePath, FileMode.Create))
                                                {

                                                    sftp.DownloadFile(strRemoteFilePath, fs);
                                                    _evt.WriteEntry("Remote Sync SFTP: File Downloaded: " + strRemoteFilePath + " Host: " + Host + " To: " + strLocalFilePath, System.Diagnostics.EventLogEntryType.Information, 1020, 10);
                                                    fs.Close();
                                                }
                                            }

                                        }
                                        

                                    }
                                }
                                catch (Exception exintry)
                                {
                                    _evt.WriteEntry("Remote Sync SFTP: File Download Error: " + DownloadPath + "/" + ftpfile.Name + " Host: " + Host + " To: " + strLocalFilePath + " Error: " + exintry.Message, System.Diagnostics.EventLogEntryType.Error, 1000, 10);
                                }

                            }

                            /*
                            
                            //Example Simple Usage
                            RemoteFiles = sftp.ListDirectory(".");
                            
                            foreach (Renci.SshNet.Sftp.SftpFile ftpfile in RemoteFiles)
                            {
                                using (FileStream fs = new FileStream(BackupFolder + "\\" + ftpfile.Name, FileMode.Create))
                                {
                                    sftp.DownloadFile(ftpfile.FullName, fs);
                                }
                                    
                            }
                            */
                        }
                        catch (Exception excon)
                        {
                            _evt.WriteEntry("Remote Sync SFTP: Host: " + Host + " Error: " + excon.Message, System.Diagnostics.EventLogEntryType.Error, 1000, 10);
                        }
                        finally
                        {
                            if (RemoteFiles != null)
                            {
                                RemoteFiles.Clear();
                            }
                            RemoteFiles = null;
                        }


                        break;
                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Remote Sync SFTP: Host: " + Host + " Error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error, 1000, 10);     
            }
            finally
            {
                if (sftp != null)
                {
                    if (sftp.IsConnected)
                    {
                        sftp.Disconnect();
                    }
                    sftp.Dispose();
                }
            }
        }

        /// <summary>
        /// Validates any FTPS certificate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private static bool ValidateTestServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Accept any certificate
            return true;
        }

        /// <summary>
        /// Validates only valid FTPS certificates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            //Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers. 
            return false;
        }

        /// <summary>
        /// FTPS procedure to upload or download files
        /// </summary>
        /// <param name="blShuttingDown"></param>
        public void FTPs(ref bool blShuttingDown)
        {
            FTPSClient FTPS = null;
            bool blFileFound = false;
            bool blOverwriteFile = false;
            try
            {
                FTPS = new FTPSClient();
                AES256 aes = new AES256(ep);
                string upassword = aes.Decrypt(_password);
                
                if (Protocol == ProtocolOptions.FTP)
                {
                    FTPS.Connect(Host, Port, new NetworkCredential(Username, upassword), ESSLSupportMode.ClearText, null, null, 0, 0, 0, Timeout);
                }
                else
                {
                    if (AllowAnyCertificate)
                    {
                        if (Protocol == ProtocolOptions.FTPsExplicit)
                        {
                            FTPS.Connect(Host, Port, new NetworkCredential(Username, upassword), ESSLSupportMode.CredentialsRequired | ESSLSupportMode.DataChannelRequested, new RemoteCertificateValidationCallback(ValidateTestServerCertificate), null, 0, 0, 0, Timeout);

                        }
                        else
                        {
                            FTPS.Connect(Host, Port, new NetworkCredential(Username, upassword), ESSLSupportMode.Implicit, new RemoteCertificateValidationCallback(ValidateTestServerCertificate), null, 0, 0, 0, Timeout);

                        }

                    }
                    else
                    {
                        if (Protocol == ProtocolOptions.FTPsExplicit)
                        {
                            FTPS.Connect(Host, Port, new NetworkCredential(Username, upassword), ESSLSupportMode.CredentialsRequired | ESSLSupportMode.DataChannelRequested, new RemoteCertificateValidationCallback(ValidateServerCertificate), null, 0, 0, 0, Timeout);
                        }
                        else
                        {
                            FTPS.Connect(Host, Port, new NetworkCredential(Username, upassword), ESSLSupportMode.Implicit, new RemoteCertificateValidationCallback(ValidateServerCertificate), null, 0, 0, 0, Timeout);
                        }
                    }
                }

                _evt.WriteEntry("Remote Sync: FTPS Connected Successfully to: " + Host, System.Diagnostics.EventLogEntryType.Information, 2005, 20);
                                
                upassword = "";
                switch (TransferDirection)
                {
                    case TransferDirectionOptions.Upload:
                        BackupFolder = BackupFolder.Replace("\\\\", "\\");
                        IList<DirectoryListItem> RemoteFilesU=null;

                        try
                        {
                            FTPS.SetCurrentDirectory(RemoteDirectory);
                            RemoteFilesU = FTPS.GetDirectoryList();
                            UploadFiles = Common.WalkDirectory(BackupFolder, ref blShuttingDown, FileNameFilter);

                            //CreateRemote Directories
                            foreach (DirectoryInfo dir in Common.GetAllDirectories(BackupFolder))
                            {
                                string strRemotePath = "";
                                strRemotePath=Common.RemotePathCombine(RemoteDirectory, dir.FullName, BackupFolder);
                                if (blShuttingDown)
                                {
                                    throw new Exception("Shutting Down");
                                }

                                if (blShuttingDown)
                                {

                                    _evt.WriteEntry("Remote Sync: Shutting down about to possibly create a folder on Host: " + Host + " Folder: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 2040, 20);
                                    return;
                                }

                                try
                                {
                                    FTPS.MakeDir(strRemotePath);
                                    _evt.WriteEntry("Remote Sync: Folder Created on " + Host + " : " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 2040, 20);
                                }
                                catch (Exception)
                                {
                                }

                                //FTPS.SetCurrentDirectory(strRemotePath);
                            }

                            //Upload Each File
                            foreach (FileInfo fileU in UploadFiles)
                            {
                                if (blShuttingDown)
                                {
                                    throw new Exception("Shutting Down");
                                }
                                try
                                {
                                    string strRemotePath = "";

                                    strRemotePath = Common.RemotePathCombine(RemoteDirectory, fileU.DirectoryName, BackupFolder);
                                    strRemotePath = Common.RemotePathCombine(strRemotePath, fileU.Name);

                                    blFileFound = false;
                                    blOverwriteFile = false;
                                    try
                                    {

                                        if (FTPS.GetFileTransferSize(strRemotePath) > 0)
                                        {
                                            blFileFound = true;
                                            if (!(/*(fileU.LastAccessTimeUtc == FTPS.GetFileModificationTime(strRemotePath)) &&*/ ((ulong)fileU.Length == FTPS.GetFileTransferSize(strRemotePath))))
                                            {
                                                blOverwriteFile = true;
                                            }

                                        }
                                    }
                                    catch (Exception)
                                    {
                                        //File Not found No Action Necessary on Error
                                    }

                                    try
                                    {
                                        if (FTPS.GetFileTransferSize(strRemotePath + ".7z") > 0)
                                        {
                                            blFileFound = true;
                                            //7z file exists but there is no current way to compare the 7zipped file vs the non compressed file so the user overwrite option will force the appropriate action
                                            blOverwriteFile = true;
                                            /*
                                            if (!((fileU.LastAccessTimeUtc == FTPS.GetFileModificationTime(strRemotePath + ".7z")) && ((ulong)fileU.Length == FTPS.GetFileTransferSize(strRemotePath + ".7z"))))
                                            {
                                                blOverwriteFile = true;
                                            }
                                            */

                                        }
                                    }
                                    catch (Exception)
                                    {
                                        //File Not Found No Action Necessary on Error
                                    }
                                    if (blShuttingDown)
                                    {

                                        _evt.WriteEntry("Remote Sync FTPS: Shutting Down about to possible upload a file: " + fileU.FullName + " Host: " + Host + " To: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 2010, 20);
                                        return;
                                    }
                                    
                                    if ((!blFileFound || blOverwriteFile || (Overwrite == OverwriteOptions.ForceOverwrite && blFileFound)) && !(Overwrite == OverwriteOptions.NoOverwrite && blFileFound))
                                    {
                                        //This Uploads the file
                                        FTPS.PutFile(fileU.FullName, strRemotePath);
                                        _evt.WriteEntry("Remote Sync FTPS: File Uploaded: " + fileU.FullName + " Host: " + Host + " To: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 2010, 20);

                                    }
                                    
                                }
                                catch (Exception exp)
                                {
                                    _evt.WriteEntry("Remote Sync FTPS: Host:" + Host + " Upload FileName: " + fileU.FullName + " Error: " + exp.Message, System.Diagnostics.EventLogEntryType.Error, 2010, 20);
                                }

                            }
                        }
                        catch (Exception exu)
                        {
                            _evt.WriteEntry("Remote Sync FTPS: Upload Error on Host:" + Host + " Error: " + exu.Message, System.Diagnostics.EventLogEntryType.Error, 2010, 20);
                        }
                        finally
                        {
                            if (RemoteFilesU != null)
                            {
                                RemoteFilesU.Clear();
                            }
                            if (UploadFiles != null)
                            {
                                UploadFiles.Clear();
                            }
                            RemoteFilesU = null;
                            UploadFiles = null;

                        }
                        break;
                    case TransferDirectionOptions.Download:
                        
                        List<RemoteFile> RemoteFilesD=null;

                        try
                        {
                            FTPS.SetCurrentDirectory(RemoteDirectory);
                            RemoteFilesD = Common.GetRemoteDirectories(RemoteDirectory, FTPS, "");
                            Common.CreateLocalFolderStructure(RemoteFilesD, BackupFolder);
                            foreach (RemoteFile FileD in RemoteFilesD)
                            {
                                string strLocalFile = "";
                                string strRemoteFile = "";

                                if (blShuttingDown)
                                {
                                    throw new Exception("Shutting Down");
                                }
                                try
                                {
                                    if (!FileD.IsDirectory)
                                    {


                                        strLocalFile = Common.WindowsPathCombine(BackupFolder, FileD.ParentDirectory);
                                        strLocalFile = Common.WindowsPathCombine(strLocalFile, FileD.Name);
                                        strRemoteFile = FileD.FullName;

                                        if (Common.DownloadFile(strLocalFile, strRemoteFile, FileD, Overwrite))
                                        {
                                            if ((!blFileFound || blOverwriteFile || (Overwrite == OverwriteOptions.ForceOverwrite && blFileFound)) && !(Overwrite == OverwriteOptions.NoOverwrite && blFileFound))
                                            {
                                                if (Common.FixNullstring(FileNameFilter) != "" && Common.VerifyPattern(FileNameFilter))
                                                {
                                                    if (Common.FileNameMatchesPattern(FileNameFilter, FileD.Name))
                                                    {
                                                        FTPS.GetFile(strRemoteFile, strLocalFile);
                                                        _evt.WriteEntry("Remote Sync FTPS: File Downloaded: " + strRemoteFile + " Host: " + Host + " To: " + strLocalFile, System.Diagnostics.EventLogEntryType.Information, 2020, 20);
                                                    }
                                                }
                                                else
                                                {
                                                    FTPS.GetFile(strRemoteFile, strLocalFile);
                                                    _evt.WriteEntry("Remote Sync FTPS: File Downloaded: " + strRemoteFile + " Host: " + Host + " To: " + strLocalFile, System.Diagnostics.EventLogEntryType.Information, 2020, 20);
                                                }
                                            }
                                                
                                        }
                                        

                                    }
                                }
                                catch (Exception exdi)
                                {
                                    _evt.WriteEntry("Remote Sync FTPS: File Download Error: " + strRemoteFile + " Host: " + Host + " To: " + strLocalFile + " Error: " + exdi.Message, System.Diagnostics.EventLogEntryType.Error, 2020, 20);
                                }

                            }
                        }
                        catch (Exception exd)
                        {
                            _evt.WriteEntry("Remote Sync FTPS: Download Error: Host: " + Host + " Error: " + exd.Message, System.Diagnostics.EventLogEntryType.Error, 2020, 20);
                        }
                        finally
                        {
                            if (RemoteFilesD !=null)
                            {
                                RemoteFilesD.Clear();
                            }
                            RemoteFilesD = null;
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Remote Sync FTPS: Error: Host: " + Host + " Error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error, 2000, 20);    
            }
            finally
            {
                if (FTPS != null)
                {
                    try
                    {
                        FTPS.Close();
                    }
                    catch (Exception)
                    {

                    }

                    FTPS.Dispose();
                    FTPS = null;
                }
            }

        }
        #endregion

    }

    
}
