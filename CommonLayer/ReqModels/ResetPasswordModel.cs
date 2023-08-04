using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ReqModels
{
    public class ResetPasswordModel
    {
        public string password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
