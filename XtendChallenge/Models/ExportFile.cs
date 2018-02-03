using System.Collections.Generic;

namespace XtendChallenge.Models
{
    public class ExportFile
    {
        public string Content { get; set; }
        public string FileName { get; set; }

        public override bool Equals(object obj)
        {
            var file = obj as ExportFile;
            return file != null &&
                   Content == file.Content &&
                   FileName == file.FileName;
        }

        public override int GetHashCode()
        {
            var hashCode = -687085254;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Content);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FileName);
            return hashCode;
        }
    }
}