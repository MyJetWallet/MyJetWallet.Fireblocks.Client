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

5.  Add to enum AssetTypeResponseType:
   
        [System.Runtime.Serialization.EnumMember(Value = @"TON_ASSET")]
        TON_ASSET = 11,

        [System.Runtime.Serialization.EnumMember(Value = @"ETH_CONTRACT")]
        ETH_CONTRACT = 12

6. Add to enum ExchangeType:
     
        [System.Runtime.Serialization.EnumMember(Value = @"BYBIT_V2")]
        BYBIT_V2 = 26,
