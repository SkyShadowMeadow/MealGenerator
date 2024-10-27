using Models;

namespace Services;

public class RecipeService(HttpClient httpClient)
{
    public async Task<MealResponseModel> GenerateRecipe(MealRequestModel request)
        {
            var prompt = $"Create a {request.TypeOfMeal} recipe with these ingredients: {string.Join(", ", request.Ingredients)}.";
            var chatRequest = new { prompt, max_tokens = 150 };

            var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/completions", chatRequest);
            var chatResponse = await response.Content.ReadFromJsonAsync<ChatGPTResponse>();

            return new MealResponseModel()
            {
                MealTitle = $"Generated {request.TypeOfMeal} recipe",
                Instructions = chatResponse.choices[0].text,
                ImageUrl = string.Empty
            };
        }

        public async Task<string> GenerateImageAsync(string recipeDescription)
        {
            var imageRequest = new { prompt = recipeDescription };
            var imageResponse =
                await httpClient.PostAsJsonAsync("https://api.openai.com/v1/images/generations", imageRequest);
            var imageResult = await imageResponse.Content.ReadFromJsonAsync<ImageResponse>();

            return imageResult.data[0].url;
        }
    }