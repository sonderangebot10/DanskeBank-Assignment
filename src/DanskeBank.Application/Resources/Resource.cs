using System;

namespace DanskeBank.Application.Resources
{
    /// <summary>
    /// Shared text messages.
    /// </summary>
    public static partial class Resources
    {
        /// <summary>
        /// Unexpected server error[. Details: A].
        /// </summary>
        public static string ERR_Unexpected_Server_Error(string details = null)
            => $"Unexpected server error{(details != null ? ". Details: " + details : string.Empty)}.";

        /// <summary>
        /// Unexpected integration error[. Request: A][. Details: B].
        /// </summary>
        public static string ERR_Unexpected_Integration_Error(string request = null, string details = null)
            =>
                $"Unexpected server error{(request != null ? ". Details: " + request : string.Empty)}{(details != null ? ". Details: " + details : string.Empty)}.";

        /// <summary>
        /// Invalid persistent cast from type A to type B.
        /// </summary>
        public static string ERR_Invalid_Persistent_Cast(object persistentFrom, object persistentTo)
            =>
                $"Invalid persistent cast from type '{(persistentFrom == null ? "NULL" : persistentFrom.GetType().Name)}' to '{(persistentTo == null ? "NULL" : persistentTo.GetType().Name)}'.";

        /// <summary>
        /// Persistent of type A cannot be null.
        /// </summary>
        public static string ERR_Null_Persistent(Type expected)
            => $"Persistent of type '{expected.Name}' cannot be NULL.";

        /// <summary>
        /// Persistent A of type B cannot be null.
        /// </summary>
        public static string ERR_Null_Persistent(string paramName, Type expected)
            => $"Persistent '{paramName}' of type '{expected.Name}' cannot be NULL.";

        /// <summary>
        /// The model provided is incomplete.
        /// </summary>
        public static string ERR_Incomplete_Request_Model()
            => "The model provided is incomplete.";

        /// <summary>
        /// Unable to create a new instance of persistent A.
        /// </summary>
        public static string ERR_Unable_Create_Persistent(Type expected)
            => $"Unable to create a new instance of persistent '{expected.Name}'.";

        /// <summary>
        /// Unable to update an existing instance of persistent A.
        /// </summary>
        public static string ERR_Unable_Update_Persistent(Type expected)
            => $"Unable to update an existing instance of persistent '{expected.Name}'.";

        /// <summary>
        /// Unable to delete an instance of persistent A.
        /// </summary>
        public static string ERR_Unable_Delete_Persistent(Type expected)
            => $"Unable to delete an instance of persistent '{expected.Name}'.";

        /// <summary>
        /// Error parsing json: EX_MESSAGE.
        /// </summary>
        public static string ERR_Parsing_Json(Exception ex)
            => $"Error parsing json: {ex.Message}.";

        /// <summary>
        /// Persistent object of type A not found.
        /// </summary>
        public static string ERR_Persistent_Not_Found(Type expected)
            => $"Persistent object of type '{expected.Name}' not found.";

        /// <summary>
        /// Unable to find persistent A of type B.
        /// </summary>
        public static string ERR_Persistent_Not_Found(string identifier, Type expected)
            => $"Unable to find persistent '{identifier}' of type '{expected.Name}'.";

        /// <summary>
        /// Unable to find persistent of type A for B :: C.
        /// </summary>
        public static string ERR_Persistent_Not_Found(string filterMetadata, string filterDetails, Type expected)
            => $"Unable to find persistent of type '{expected.Name}' for '{filterMetadata} :: {filterDetails}'.";

        /// <summary>
        /// Type ''<paramref name="expected"/>' not found.
        /// </summary>
        public static string ERR_Type_Not_Found(Type expected)
            => $"Type '{expected.Name}' not found.";

        /// <summary>
        /// Object of type '<paramref name="expected"/>' not found, expected base type '<paramref name="baseType"/>'.
        /// </summary>
        public static string ERR_Type_Not_Found(string expected, Type baseType)
            => $"Object of type '{expected}' not found, expected base type '{baseType.Name}'.";

        /// <summary>
        /// Duplicate record found for '<paramref name="expected"/>'.
        /// </summary>
        public static string ERR_Duplicate_Record_Found(Type expected)
            => $"Duplicate record found for '{expected.Name}'.";

        /// <summary>
        /// Permission denied.
        /// </summary>
        public static string ERR_Permission_Denied()
            => string.Format("Permission denied.");

        /// <summary>
        /// Permission denied.
        /// </summary>
        public static string ERR_Permission_Denied(string details)
            => string.Format(ERR_Permission_Denied() + (string.IsNullOrEmpty(details) ? "" : " " + details));

        /// <summary>
        /// Cannot delete the root user.
        /// </summary>
        public static string ERR_Unable_Delete_Root_User()
            => "Cannot delete the root user.";

        /// <summary>
        /// Request body cannot be empty or whitespace.
        /// </summary>
        public static string ERR_Request_Body_Cannot_Be_Empty()
            => "Request body cannot be empty or whitespace.";

        /// <summary>
        /// Invalid argument '<paramref name="argument"/>', expected to be: not null.
        /// </summary>
        public static string ERR_Argument_Exception(string argument)
            => ERR_Argument_Exception(argument, "not null");

        /// <summary>
        /// Invalid argument '<paramref name="argument"/>', expected to be: <paramref name="expectedValue"/>.
        /// </summary>
        public static string ERR_Argument_Exception(string argument, string expectedValue)
            => $"Invalid argument '{argument}', expected to be: {expectedValue}.";
    }
}
