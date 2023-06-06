/* 
 * Demo Platform
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = dhango.Web.Sdk.Client.SwaggerDateConverter;

namespace dhango.Web.Sdk.Model
{
    /// <summary>
    /// Includes a collection of transaction records.
    /// </summary>
    [DataContract]
        public partial class GetTransactionsResponse :  IEquatable<GetTransactionsResponse>, IValidatableObject
    {
        /// <summary>
        /// A collection of transaction records that match the search criteria.
        /// </summary>
        /// <value>A collection of transaction records that match the search criteria.</value>
        [DataMember(Name="transactions", EmitDefaultValue=false)]
        public List<GetTransactionResponse> Transactions { get; set; }

        /// <summary>
        /// The total number of records found that match the search criteria.
        /// </summary>
        /// <value>The total number of records found that match the search criteria.</value>
        [DataMember(Name="totalRecords", EmitDefaultValue=false)]
        public int? TotalRecords { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GetTransactionsResponse {\n");
            sb.Append("  Transactions: ").Append(Transactions).Append("\n");
            sb.Append("  TotalRecords: ").Append(TotalRecords).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as GetTransactionsResponse);
        }

        /// <summary>
        /// Returns true if GetTransactionsResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of GetTransactionsResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetTransactionsResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Transactions == input.Transactions ||
                    this.Transactions != null &&
                    input.Transactions != null &&
                    this.Transactions.SequenceEqual(input.Transactions)
                ) && 
                (
                    this.TotalRecords == input.TotalRecords ||
                    (this.TotalRecords != null &&
                    this.TotalRecords.Equals(input.TotalRecords))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Transactions != null)
                    hashCode = hashCode * 59 + this.Transactions.GetHashCode();
                if (this.TotalRecords != null)
                    hashCode = hashCode * 59 + this.TotalRecords.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
