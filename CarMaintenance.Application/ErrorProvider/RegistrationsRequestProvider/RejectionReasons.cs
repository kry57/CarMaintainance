using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.ErrorProvider.RegistrationsRequestProvider
{
    public static class RejectionReasons
    {
        public const string TimeSlotBooked = "This time slot is already booked.";
        public const string AppointmentPassed = "The appointment time passed before the request was processed.";
    }
}
