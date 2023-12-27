using BbgEducation.Domain.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Domain.Common;
public abstract class Entity
{
    public DateTime? created_datetime { get; set; }
    public DateTime? updated_datetime { get; set; }
    public DateTime? inactivated_datetime { get; set; }
    public User? inactivated_user { get; set; }

    public abstract bool isNew();

    protected Entity() {

    }
}
