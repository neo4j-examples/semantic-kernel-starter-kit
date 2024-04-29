using Azure.AI.OpenAI;

public class BasicChat
{
  private OpenAIClient client;

  private ChatCompletionsOptions chatCompletionsOptions;
  public BasicChat(OpenAIClient client, 
    String modelName = "gpt-3.5-turbo", 
    String prompt = "You are a helpful assistant. You will talk like a pirate.")
  {
    this.client = client;
    this.chatCompletionsOptions = new ChatCompletionsOptions()
    {
      DeploymentName = modelName, 
      Messages =
      {
          // The system message represents instructions or other guidance about how the assistant should behave
          new ChatRequestSystemMessage(prompt),
      }
    };

  }
    
    public string Say(string message)
    {
      chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(message));
      Response<ChatCompletions> response = client.GetChatCompletions(chatCompletionsOptions);
      ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
      chatCompletionsOptions.Messages.Add(new ChatRequestAssistantMessage(responseMessage.Content));

      return $"{responseMessage.Content}";
    }
}