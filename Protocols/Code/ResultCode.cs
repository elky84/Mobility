using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobility.Protocols.Code
{
    public enum ResultCode
    {
        Success,
        Fail,
        AlreadySignedEmail,
        NotFoundMemberByEmail,
        NotMatchedPassword,
        NotFoundMember,
        NotFoundTaxi,
        AlreadyRegisteredCall,
        AlreadyReceivedCall,
        AlreadyReceivedCallByTaxi,
        NotFoundCall,
        CallCompleteOnlyReceive,
        CallReceiveOnlyWait,
        CallOnlyUser,
        TaxiRegisterOnlyDriver,
        UnknownException,
    }
}
