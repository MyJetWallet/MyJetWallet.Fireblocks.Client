using System;

namespace MyJetWallet.Fireblocks.Client
{
    public class CreateConfigOperationRequest { }

    public partial class CreateConversionConfigOperationRequest : CreateConfigOperationRequest { }

    public partial class CreateTransferConfigOperationRequest : CreateConfigOperationRequest { }

    public partial class CreateDisbursementConfigOperationRequest : CreateConfigOperationRequest { }



    public class ConfigOperation { }

    public partial class ConversionConfigOperation : ConfigOperation { }

    public partial class TransferConfigOperation : ConfigOperation { }

    public partial class DisbursementConfigOperation : ConfigOperation { }



    public class ConfigOperationSnapshot { }

    public partial class ConfigConversionOperationSnapshot : ConfigOperationSnapshot { }

    public partial class ConfigTransferOperationSnapshot : ConfigOperationSnapshot { }

    public partial class ConfigDisbursementOperationSnapshot : ConfigOperationSnapshot { }



    public class WorkflowExecutionOperation { }

    public partial class ExecutionScreeningOperation : WorkflowExecutionOperation { }

    public partial class ExecutionConversionOperation : WorkflowExecutionOperation { }

    public partial class ExecutionTransferOperation : WorkflowExecutionOperation { }

    public partial class ExecutionDisbursementOperation : WorkflowExecutionOperation { }

    public abstract class Destination { }
}
