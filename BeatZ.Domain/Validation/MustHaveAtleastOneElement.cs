using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatZ.Domain.Validation
{
    public class MustHaveAtleastOneElement : ValidationAttribute
    {
        public override bool IsValid (object value)
        {
            return value is IList { Count: > 0 };
        }
    }
}
