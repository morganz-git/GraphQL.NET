namespace RestSharpSpecflow.Steps;

[Binding]
public sealed class BasicOperation
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;

    public BasicOperation(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"I perform a GET operation of ""(.*)""")]
    public void GivenIPerformAgetOperationOf(string p0, Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"I should get the product name as ""(.*)""")]
    public void GivenIShouldGetTheProductNameAs(string keyboard)
    {
        ScenarioContext.StepIsPending();
    }
}