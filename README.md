![Nuget version](https://img.shields.io/nuget/v/MyJetWallet.Fireblocks.Client?label=MyJetWallet.Fireblocks.Client&style=social)

Generate Fireblocks API Client flow

1. Update api-spec-2.yaml
   
2. Genereate ApiClients.cs from console:
	
	dotnet new tool-manifest --force

	dotnet tool install NSwag.ConsoleCore

	dotnet nswag version

	dotnet nswag run nswag.json
 
4. In ApiClients.cs replace:
   - status_ == 5XX
     ->     status_ >= 500 && status_ < 600
     
   - public ExternalCorrelationData ExternalCorrelationData
     ->     public CorrelationData ExternalCorrelationData
     
   - System.Threading.Tasks.Task<Response<WorkflowExecution>> ExecutePostAsync
     ->     System.Threading.Tasks.Task<Response<WorkflowExecution>> WorkflowExecutePostAsync
     
   - System.Threading.Tasks.Task<Response<DelegationDto>> PositionsGetAsync(string id
     ->     System.Threading.Tasks.Task<Response<DelegationDto>> PositionsGetByIdAsync(string id
     
   - System.Collections.Generic.List<InstructionSet>
     ->     System.Collections.Generic.List<instructionSet>

   In class CreateWorkflowConfigurationRequest:
    - public System.Collections.Generic.List<ConfigOperations> ConfigOperations { get; set; } = new System.Collections.Generic.List<ConfigOperations>();
      ->    public System.Collections.Generic.List<CreateConfigOperationRequest> ConfigOperations { get; set; } = new System.Collections.Generic.List<CreateConfigOperationRequest>();
      
   In class WorkflowConfiguration:
    - public System.Collections.Generic.List<configOperations> ConfigOperations { get; set; } = new System.Collections.Generic.List<configOperations>();
      ->    public System.Collections.Generic.List<ConfigOperation> ConfigOperations { get; set; } = new System.Collections.Generic.List<ConfigOperation>();
            
   In class WorkflowConfigurationSnapshot:
    - public System.Collections.Generic.List<configOperations2> ConfigOperations { get; set; } = new System.Collections.Generic.List<configOperations2>();
      ->    public System.Collections.Generic.List<ConfigOperationSnapshot> ConfigOperations { get; set; } = new System.Collections.Generic.List<ConfigOperationSnapshot>();
                 
   In class WorkflowExecution:
    - public System.Collections.Generic.List<ExecutionOperations> ExecutionOperations { get; set; } = new System.Collections.Generic.List<ExecutionOperations>();
      ->    public System.Collections.Generic.List<WorkflowExecutionOperation> ExecutionOperations { get; set; } = new System.Collections.Generic.List<WorkflowExecutionOperation>();

   In class class TransactionResponse:
    - [Newtonsoft.Json.JsonProperty("externalTxId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
      ->   [Newtonsoft.Json.JsonProperty("externalTxId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

    - [Newtonsoft.Json.JsonProperty("customerRefId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
      ->   [Newtonsoft.Json.JsonProperty("customerRefId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

    - [Newtonsoft.Json.JsonProperty("requestedAmount", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
      ->   [Newtonsoft.Json.JsonProperty("requestedAmount", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    - 
    - [Newtonsoft.Json.JsonProperty("amount", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
      ->   [Newtonsoft.Json.JsonProperty("amount", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    - 
    - [Newtonsoft.Json.JsonProperty("netAmount", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
      ->   [Newtonsoft.Json.JsonProperty("netAmount", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    - 
    - [Newtonsoft.Json.JsonProperty("serviceFee", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
      ->   [Newtonsoft.Json.JsonProperty("serviceFee", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    - 
    - [Newtonsoft.Json.JsonProperty("fee", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
      ->   [Newtonsoft.Json.JsonProperty("fee", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    - 
    - [Newtonsoft.Json.JsonProperty("networkFee", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
      ->   [Newtonsoft.Json.JsonProperty("networkFee", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

   In class class TransactionsClient:
    - public partial class TransactionsClient : BaseClient, ITransactionsClient
      ->   public partial class TransactionsClient : BaseClient, ITransactionsClient, ITransactionsAdminClient, ITransactionsSignerClient
      
6.  Add to enum AssetTypeResponseType:
   
        [System.Runtime.Serialization.EnumMember(Value = @"TON_ASSET")]
        TON_ASSET = 11,

        [System.Runtime.Serialization.EnumMember(Value = @"ETH_CONTRACT")]
        ETH_CONTRACT = 12,
    
        [System.Runtime.Serialization.EnumMember(Value = @"XRP_ASSET")]
        XRP_ASSET = 13,
    
        [System.Runtime.Serialization.EnumMember(Value = @"ERC721")]
        ERC721 = 14,

        [System.Runtime.Serialization.EnumMember(Value = @"ERC1155")]
        ERC1155 = 15

        [System.Runtime.Serialization.EnumMember(Value = @"TOKEN")]
        TOKEN = 16

        [System.Runtime.Serialization.EnumMember(Value = @"SUI_ASSET")]
        SUI_ASSET = 17,

8. Add to enum ExchangeType:
     
        [System.Runtime.Serialization.EnumMember(Value = @"BYBIT_V2")]
        BYBIT_V2 = 26,

9. fix API specification:

  In class RawMessageData
   - public `RawMessageDataAlgorithm? Algorithm { get; set; }` Algorithm should be nullable

  In class SourceTransferPeerPath:
   - `public TransferPeerPathSubType? SubType { get; set; }` SubType should be nullable

  In class UnsignedMessage:
   - `public int? Bip44addressIndex { get; set; }` Bip44addressIndex should be nullable
   - `public double? Bip44change { get; set; }` Bip44change should be nullable
   - `public string Content { get; set; }` Content should be nullable and typed like object

   In class AmountInfo:
   - `public string Amount { get; set; }` Amount should be nullable
   - `public string RequestedAmount { get; set; }` RequestedAmount should be nullable
   - `public string NetAmount { get; set; }` NetAmount should be nullable
   - `public string AmountUSD { get; set; }` AmountUSD should be nullable