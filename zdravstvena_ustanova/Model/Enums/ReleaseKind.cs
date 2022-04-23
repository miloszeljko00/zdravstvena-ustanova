// File:    AccountType.cs
// Author:  miloszeljko00
// Created: nedelja, 10. april 2022. 10:57:29
// Purpose: Definition of Enum AccountType

using System;

namespace Model.Enums
{
   public enum ReleaseKind
   {
      HOME_RELEASE,
      RELEASE_TO_OTHER_CLINIC,
      STATISTICAL_RELEASE,
      DOCTORSELF_REQUEST_RELEASE_SPECIALIST,
      DIED
   }
}