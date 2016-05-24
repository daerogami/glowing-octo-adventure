using System;

namespace image_validator.classes
{
    internal class FilePairValidationInfo
    {
        private bool _hasValidMatchingXML;
        private bool _filePairsAreValid;
        public EXIF ExifData { get; private set; }
        public string ValidationStatus { get; private set; }
        
        public FilePairValidationInfo()
        {
            ExifData = null;
            _hasValidMatchingXML = false;
            _filePairsAreValid = false;
            ValidationStatus = "No matching image found";
        }

        public FilePairValidationInfo(EXIF exifDataInit)
        {
            ExifData = exifDataInit;
            _hasValidMatchingXML = false;
            _filePairsAreValid = false;
            ValidationStatus = "No matching XML found";
        }

        internal void SetXMLValidity(bool isValid)
        {
            _hasValidMatchingXML = isValid;
            ValidationStatus = "Unverified matching XML and Image";
        }

        public string GetAdventureValidation()
        {
            return "IMAGE XML " + (_filePairsAreValid?"PASS":"FAIL");
        }
    }
}