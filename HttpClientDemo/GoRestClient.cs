using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    public class GoRestClient
    {
        private readonly RestClient _restClient;
        private const string token = "4bb157b28c5c78bf8cc209b3fe5c23330cccad0aa8b1d759b983f061c7ddc264";

        public GoRestClient()
        {
            _restClient = new RestClient("https://gorest.co.in/");
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    {"Accept", "application/json" },
                    {"User-Agent", "Cherry HttpClient" },
                };

                var response = await _restClient.GetRequestAsync(headers, "public-api/users");

                response.EnsureSuccessStatusCode();

                GetUsersResponse responseObject = JsonConvert.DeserializeObject<GetUsersResponse>(await response.Content.ReadAsStringAsync());

                List<User> users = new List<User>();

                foreach (var data in responseObject.data)
                {
                    users.Add(
                        new User
                        {
                            name = data.name,
                            gender = data.gender,
                            email = data.email,
                            status = data.status
                        });
                }

                return users;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("GetRequest to get users from the 'gorest.in' website failed : " + e.Message);
                throw e;
            }
        }

        public async Task<User> CreateUserAsync()
        {
            try
            {
                var headers = new Dictionary<string, string>
                {
                    {"Accept", "application/json" },
                    {"Authorization", "Bearer " + token },
                    {"User-Agent", "Cherry HttpCliet" },
                };

                var user = new User
                {
                    name = "Tenali Ramakrishna",
                    gender = "Male",
                    email = "tenali.ramakrishna@162ce.com",
                    status = "Active"
                };

                var response = await _restClient.PostRequestAsync(headers, user, "public-api/users/");
                response.EnsureSuccessStatusCode();
                var lol = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<CreateOrUpdateResponse>(await response.Content.ReadAsStringAsync()).data;

                return new User
                {
                    name = responseObject.name,
                    email = responseObject.email,
                    gender = responseObject.gender,
                    status = responseObject.status
                };
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("POST requst to create user in 'gorest.in' website failed : " + e.Message);

                throw e;
            }

        }

        public async Task<User> UpdateUserAsync()
        {
            try
            {
                var headers = new Dictionary<string, string>
                {
                    {"Accept", "application/json" },
                    {"Authorization", "Bearer " + token },
                    {"User-Agent", "Cherry HttpCliet" },
                };

                var user = new User
                {
                    name = "Tenali Ramakrishna Updated",
                    gender = "Male",
                    email = "tenali.ramakrishna@160ce.com",
                    status = "Active"
                };

                var response = await _restClient.PatchRequestAsync(headers, user, "public-api/users/1633");
                response.EnsureSuccessStatusCode();

                var responseObject = JsonConvert.DeserializeObject<CreateOrUpdateResponse>(await response.Content.ReadAsStringAsync()).data;

                return new User
                {
                    name = responseObject.name,
                    email = responseObject.email,
                    gender = responseObject.gender,
                    status = responseObject.status
                };
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("POST requst to create user in 'gorest.in' website failed : " + e.Message);

                throw e;
            }
        }

        public async Task DeleteUsersAsync()
        {
            try
            {
                Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    {"Accept", "application/json" },
                    {"Authorization", "Bearer " + token },
                    {"User-Agent", "Cherry HttpClient" },
                };

                var response = await _restClient.DeleteRequestAsync(headers, "public-api/users/1633");

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetRequest to get users from the 'gorest.in' website failed : " + e.Message);
            }
        }

    }
}
