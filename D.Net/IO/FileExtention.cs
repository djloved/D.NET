using D.Net.Collection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace D.Net.IO
{
    public static class FileExtention
    {
        public static string CheckFolder(this string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return path;
        }
        public static void DeleteDirectory(this string directory)
        {
            try
            {
                foreach (var s in Directory.GetFiles(directory, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        File.Delete(s);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                Directory.Delete(directory, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool FileMove(this string src, string dest, bool bOverWrite = true)
        {
            if (File.Exists(dest))
            {
                if (bOverWrite)
                {
                    try
                    {
                        File.Delete(dest);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        return false;
                    }
                }
                else
                    return false;
            }
            try
            {
                File.Move(src, dest);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public static bool FileCopy(this string src, string dest, bool bOverWrite = true)
        {
            try
            {
                File.Copy(src, dest, bOverWrite);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public static bool FileDelete(this string fileName)
        {
            try
            {
                File.Delete(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
            return true;
        }
        public static bool FileExisted(this string fileName)
        {
            return File.Exists(fileName);
        }
        public static string ReadAllText(this string fileName)
        {
            return File.ReadAllText(fileName);
        }
        public static FileInfo ToFileInfo(this string fileName)
        {
            return new FileInfo(fileName);
        }
        public static DirectoryInfo ToDirInfo(this string dirName)
        {
            return new DirectoryInfo(dirName);
        }
        public static FileInfo FileCopyToDir(this string src, string destDir, bool overwrite = false)
        {
            try
            {
                FileInfo fi = new FileInfo(src);
                FileInfo nwFi = new FileInfo(Path.Combine(destDir, Path.GetFileNameWithoutExtension(fi.Name) + fi.Extension));
                if ((!nwFi.Exists) || overwrite)
                {
                    return fi.CopyTo(nwFi.FullName, overwrite);
                }
                else
                {
                    return fi.CopyTo(Path.Combine(destDir,Path.GetFileNameWithoutExtension(fi.Name) + "_" + DateTime.Now.FileStamp() + fi.Extension));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public static FileInfo FileMoveToDir(this string src, string destDir, bool overwrite = false)
        {
            FileInfo rs = src.FileCopyToDir(destDir, overwrite);
            try
            {
                if (rs != null && rs.Exists)
                    File.Delete(src);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rs;
        }
        public static string SafeFileName(this string fileName)
        {

            string tmp = Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), " "));
            return Path.GetInvalidPathChars().Aggregate(tmp, (current, c) => current.Replace(c.ToString(), " "));
        }

        public static string GetContactedFileName(string fileName, int maxlength = 100)
        {
            string fiName = Path.GetFileNameWithoutExtension(fileName);
            if (fiName.Length > maxlength)
            {
                fiName = fiName.Substring(0, maxlength);
            }
            return fiName + Path.GetExtension(fileName);
        }
        public static void SetWorkingDirectoryToExecutingAssembly()
        {
            try
            {
                FileInfo fInfo = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
                Directory.SetCurrentDirectory(fInfo.Directory.FullName);
            }
            catch (Exception ex)
            {

            }

        }
        public static bool CheckDirectory(this string dirPath, bool isNeedCreation = true)
        {
            dirPath = Path.GetFullPath(dirPath);
            if (!Directory.Exists(dirPath))
            {
                try
                {
                    Directory.CreateDirectory(dirPath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return true;
            }
            return false;
        }
        public static long CheckFileRetentionPolicy(this string dir, TimeSpan retention, string fileFilter = "*.*")
        {
            long proccesdCount = 0;
            dir.CheckDirectory(true);
            foreach (var _file in Directory.EnumerateFiles(dir, fileFilter, SearchOption.AllDirectories))
            {
                try
                {
                    FileInfo file = new FileInfo(_file);
                    if (DateTime.UtcNow - file.CreationTimeUtc > retention)
                    {
                        _file.FileDelete();
                    }
                    System.Threading.Interlocked.Increment(ref proccesdCount);
                }
                catch (Exception ex)
                {
                    //Singleton<LogMan>.Inst.doLog(ex);
                    throw (ex);
                }
            }
            return proccesdCount;
        }
        public static bool IsFileAvailable(string path)
        {
            try
            {
                if (!File.Exists(path)) return false;
                using (FileStream fs = File.Open(path, FileMode.Open)) { };
                return true;
            }
            catch (Exception ex)
            {
                //Singleton<LogMan>.Inst.doLog(ex);
                throw ex;
            }
            return false;
        }

        //public static bool CheckPermission(this string fileName, FileIOPermissionAccess access = FileIOPermissionAccess.AllAccess)
        //{
        //    bool bRes = false;
        //    try
        //    {
        //        FileIOPermission f = new FileIOPermission(access, fileName);//.Demand();
        //        f.Demand();
        //    }
        //    catch (Exception ex)
        //    {
        //        Singleton<LogMan>.Inst.doLog(ex);
        //    }
        //    return bRes;
        //}
        public static string GetSafeSharePointFileName(this string fileName, string replaced = "", int maxLength = 128)
        {
            fileName = fileName.SafeFileName();
            Regex illegalPathChars = new Regex(@"^\.|[\x00-\x1F,\x7B-\x9F,"",#,%,&,*,/,:,<,>,?,\\]+|(\.\.)+|\.$", RegexOptions.Compiled);

            fileName = illegalPathChars.Replace(Path.GetFileNameWithoutExtension(fileName.Trim()), replaced) + Path.GetExtension(fileName);
            return fileName;
        }
        public static string GetDirName(this string path)
        {
            return Path.GetDirectoryName(path);
        }
        public static string GetFileNameOnly(this string path)
        {
            return Path.GetFileName(path);
        }
        public static string GetFilenameWOExt(this string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
        public static string EntryAssemblyLoc()
        {
            string r = AppContext.BaseDirectory;
            
            //if (Assembly.GetEntryAssembly() != null)
            //    r = Assembly.GetEntryAssembly().Location.GetDirName();
            //else if (Assembly.GetCallingAssembly() != null)
            //    r = Assembly.GetCallingAssembly().Location.GetDirName();
            
            return r;
        }

        public static Dictionary<string, string> ENV_PATH = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase)
        {
            ["entry"] = EntryAssemblyLoc(),
            ["base"] = AppContext.BaseDirectory,
        };
        public static string ToCASPath(this string path, ManagedDict<string, string> extraPath = null)
        {
            if (string.IsNullOrEmpty(path))
                path = "@working";
            path = path.ToCASString(extraPath);
            if (path.StartsWith("\\\\") || path.Contains(":"))
                return path;
            return Path.GetFullPath(path);
        }
        public static string ToCASString(this string path, ManagedDict<string, string> extraPath = null)
        {
            if (extraPath != null)
                foreach (var k in extraPath)
                {
                    path = Regex.Replace(path, "{@" + k.Key + "}", k.Value.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    path = Regex.Replace(path, $"@{k.Key}", k.Value.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                }

            foreach (var k in ENV_PATH)
            {
                path = Regex.Replace(path, "{@" + k.Key + "}", k.Value.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                path = Regex.Replace(path, $"@{k.Key}", k.Value.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }

            string workingDir = Directory.GetCurrentDirectory();
            path = Regex.Replace(path, "{@working}", workingDir, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            path = Regex.Replace(path, "@working", workingDir, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return path;
        }
    }
}
