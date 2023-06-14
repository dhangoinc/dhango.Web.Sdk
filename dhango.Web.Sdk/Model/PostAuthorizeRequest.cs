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
    /// The model to generate a card authorization.
    /// </summary>
    [DataContract]
        public partial class PostAuthorizeRequest :  IEquatable<PostAuthorizeRequest>, IValidatableObject
    {        
        /// <summary>
        /// The name of the payer. This is not necessarily the same name as the card or bank account holder name.
        /// </summary>
        /// <value>The name of the payer. This is not necessarily the same name as the card or bank account holder name.</value>
        [DataMember(Name="payer", EmitDefaultValue=false)]
        public string Payer { get; set; }

        /// <summary>
        /// The user Id from the application that represents the user that owns this payment method.
        /// </summary>
        /// <value>The user Id from the application that represents the user that owns this payment method.</value>
        [DataMember(Name="userId", EmitDefaultValue=false)]
        public string UserId { get; set; }

        /// <summary>
        /// The email address of the user.
        /// </summary>
        /// <value>The email address of the user.</value>
        [DataMember(Name="emailAddress", EmitDefaultValue=false)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The token identifier for a previously saved payment method. If this is supplied, the other payment  options can be left as null.
        /// </summary>
        /// <value>The token identifier for a previously saved payment method. If this is supplied, the other payment  options can be left as null.</value>
        [DataMember(Name="tokenId", EmitDefaultValue=false)]
        public string? TokenId { get; set; }

        /// <summary>
        /// Gets or Sets Card
        /// </summary>
        [DataMember(Name="card", EmitDefaultValue=false)]
        public Card Card { get; set; }

        /// <summary>
        /// A  &lt;string, string&gt;  dictionary of any application-specific metadata to have stored with the payment record.
        /// </summary>
        /// <value>A  &lt;string, string&gt;  dictionary of any application-specific metadata to have stored with the payment record.</value>
        [DataMember(Name="metadata", EmitDefaultValue=false)]
        public Dictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// Gets or Sets BillingAddress
        /// </summary>
        [DataMember(Name="billingAddress", EmitDefaultValue=false)]
        public Address BillingAddress { get; set; }

        /// <summary>
        /// Gets or Sets ShippingAddress
        /// </summary>
        [DataMember(Name="shippingAddress", EmitDefaultValue=false)]
        public Address ShippingAddress { get; set; }

        /// <summary>
        /// The total amount being paid. This is inclusive of the payer fee, if any.
        /// </summary>
        /// <value>The total amount being paid. This is inclusive of the payer fee, if any.</value>
        [DataMember(Name="amount", EmitDefaultValue=false)]
        public double? Amount { get; set; }

        /// <summary>
        /// The amount the payer is being charged to initiate this transaction. This should only be a non-zero amount when  you are charging the payer a fee (e.g. a convenience fee or surcharge).
        /// </summary>
        /// <value>The amount the payer is being charged to initiate this transaction. This should only be a non-zero amount when  you are charging the payer a fee (e.g. a convenience fee or surcharge).</value>
        [DataMember(Name="payerFee", EmitDefaultValue=false)]
        public double? PayerFee { get; set; }

        /// <summary>
        /// The amount the platform is charging the account for this transaction. If left null, the fee will be calculated   automatically based on the fee settings on the account. This is only used as an override to that calculation.  Only the platform can set this fee.
        /// </summary>
        /// <value>The amount the platform is charging the account for this transaction. If left null, the fee will be calculated   automatically based on the fee settings on the account. This is only used as an override to that calculation.  Only the platform can set this fee.</value>
        [DataMember(Name="platformFee", EmitDefaultValue=false)]
        public double? PlatformFee { get; set; }

        /// <summary>
        /// Gets or Sets Currency
        /// </summary>
        [DataMember(Name="currency", EmitDefaultValue=false)]
        public Currency Currency { get; set; }

        /// <summary>
        /// Additional comments entered in by the payer.
        /// </summary>
        /// <value>Additional comments entered in by the payer.</value>
        [DataMember(Name="comments", EmitDefaultValue=false)]
        public string Comments { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostAuthorizeRequest {\n");
            sb.Append("  Payer: ").Append(Payer).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  EmailAddress: ").Append(EmailAddress).Append("\n");
            sb.Append("  TokenId: ").Append(TokenId).Append("\n");
            sb.Append("  Card: ").Append(Card).Append("\n");
            sb.Append("  Metadata: ").Append(Metadata).Append("\n");
            sb.Append("  BillingAddress: ").Append(BillingAddress).Append("\n");
            sb.Append("  ShippingAddress: ").Append(ShippingAddress).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  PayerFee: ").Append(PayerFee).Append("\n");
            sb.Append("  PlatformFee: ").Append(PlatformFee).Append("\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  Comments: ").Append(Comments).Append("\n");
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
            return this.Equals(input as PostAuthorizeRequest);
        }

        /// <summary>
        /// Returns true if PostAuthorizeRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of PostAuthorizeRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostAuthorizeRequest input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Payer == input.Payer ||
                    (this.Payer != null &&
                    this.Payer.Equals(input.Payer))
                ) && 
                (
                    this.UserId == input.UserId ||
                    (this.UserId != null &&
                    this.UserId.Equals(input.UserId))
                ) && 
                (
                    this.EmailAddress == input.EmailAddress ||
                    (this.EmailAddress != null &&
                    this.EmailAddress.Equals(input.EmailAddress))
                ) && 
                (
                    this.TokenId == input.TokenId ||
                    (this.TokenId != null &&
                    this.TokenId.Equals(input.TokenId))
                ) && 
                (
                    this.Card == input.Card ||
                    (this.Card != null &&
                    this.Card.Equals(input.Card))
                ) && 
                (
                    this.Metadata == input.Metadata ||
                    this.Metadata != null &&
                    input.Metadata != null &&
                    this.Metadata.SequenceEqual(input.Metadata)
                ) && 
                (
                    this.BillingAddress == input.BillingAddress ||
                    (this.BillingAddress != null &&
                    this.BillingAddress.Equals(input.BillingAddress))
                ) && 
                (
                    this.ShippingAddress == input.ShippingAddress ||
                    (this.ShippingAddress != null &&
                    this.ShippingAddress.Equals(input.ShippingAddress))
                ) && 
                (
                    this.Amount == input.Amount ||
                    (this.Amount != null &&
                    this.Amount.Equals(input.Amount))
                ) && 
                (
                    this.PayerFee == input.PayerFee ||
                    (this.PayerFee != null &&
                    this.PayerFee.Equals(input.PayerFee))
                ) && 
                (
                    this.PlatformFee == input.PlatformFee ||
                    (this.PlatformFee != null &&
                    this.PlatformFee.Equals(input.PlatformFee))
                ) && 
                (
                    this.Currency == input.Currency ||
                    (this.Currency != null &&
                    this.Currency.Equals(input.Currency))
                ) && 
                (
                    this.Comments == input.Comments ||
                    (this.Comments != null &&
                    this.Comments.Equals(input.Comments))
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
                if (this.Payer != null)
                    hashCode = hashCode * 59 + this.Payer.GetHashCode();
                if (this.UserId != null)
                    hashCode = hashCode * 59 + this.UserId.GetHashCode();
                if (this.EmailAddress != null)
                    hashCode = hashCode * 59 + this.EmailAddress.GetHashCode();
                if (this.TokenId != null)
                    hashCode = hashCode * 59 + this.TokenId.GetHashCode();
                if (this.Card != null)
                    hashCode = hashCode * 59 + this.Card.GetHashCode();
                if (this.Metadata != null)
                    hashCode = hashCode * 59 + this.Metadata.GetHashCode();
                if (this.BillingAddress != null)
                    hashCode = hashCode * 59 + this.BillingAddress.GetHashCode();
                if (this.ShippingAddress != null)
                    hashCode = hashCode * 59 + this.ShippingAddress.GetHashCode();
                if (this.Amount != null)
                    hashCode = hashCode * 59 + this.Amount.GetHashCode();
                if (this.PayerFee != null)
                    hashCode = hashCode * 59 + this.PayerFee.GetHashCode();
                if (this.PlatformFee != null)
                    hashCode = hashCode * 59 + this.PlatformFee.GetHashCode();
                if (this.Currency != null)
                    hashCode = hashCode * 59 + this.Currency.GetHashCode();
                if (this.Comments != null)
                    hashCode = hashCode * 59 + this.Comments.GetHashCode();
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
