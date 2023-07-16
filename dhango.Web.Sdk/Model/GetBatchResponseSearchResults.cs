/* 
 * Demo Platform
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace dhango.Web.Sdk.Model
{
    /// <summary>
    /// Represents search results to display on a page.
    /// </summary>
    [DataContract]
        public partial class GetBatchResponseSearchResults :  IEquatable<GetBatchResponseSearchResults>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetBatchResponseSearchResults" /> class.
        /// </summary>
        /// <param name="records">The list of records to display in the search results..</param>
        /// <param name="totalRecords">The total number of records in the search results..</param>
        public GetBatchResponseSearchResults(List<GetBatchResponse> records = default(List<GetBatchResponse>), int? totalRecords = default(int?))
        {
            this.Records = records;
            this.TotalRecords = totalRecords;
        }
        
        /// <summary>
        /// The list of records to display in the search results.
        /// </summary>
        /// <value>The list of records to display in the search results.</value>
        [DataMember(Name="records", EmitDefaultValue=false)]
        public List<GetBatchResponse> Records { get; set; }

        /// <summary>
        /// The total number of records in the search results.
        /// </summary>
        /// <value>The total number of records in the search results.</value>
        [DataMember(Name="totalRecords", EmitDefaultValue=false)]
        public int? TotalRecords { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GetBatchResponseSearchResults {\n");
            sb.Append("  Records: ").Append(Records).Append("\n");
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
            return this.Equals(input as GetBatchResponseSearchResults);
        }

        /// <summary>
        /// Returns true if GetBatchResponseSearchResults instances are equal
        /// </summary>
        /// <param name="input">Instance of GetBatchResponseSearchResults to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetBatchResponseSearchResults input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Records == input.Records ||
                    this.Records != null &&
                    input.Records != null &&
                    this.Records.SequenceEqual(input.Records)
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
                if (this.Records != null)
                    hashCode = hashCode * 59 + this.Records.GetHashCode();
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