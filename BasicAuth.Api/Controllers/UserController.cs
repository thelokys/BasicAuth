namespace BasicAuth.Api.Controllers
{
    using System.Threading.Tasks;
    using BasicAuth.Api.Models.ViewModel;
    using BasicAuth.Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Driver;

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private IMongoCollection<User> Users;

        public UserController(User model)
        {
            this.Users = model.Users;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(
                await this.Users.Find<User>(Builders<User>.Filter.Empty)
                .ToListAsync()
            );
        }

        [HttpGet("{id}", Name = "showUser")]
        public async Task<IActionResult> Show(string id)
        {
            var userFound = await this.Users
                .Find<User>(user => user.Id == id)
                .FirstOrDefaultAsync();

            if (userFound == null)
            {
                return NotFound(id);
            }

            return Ok(userFound);
        }

        [HttpPost]
        public async Task<IActionResult> Store(User newUser)
        {
            var userFound = this.Users
                .Find<User>(user => user.Email == newUser.Email)
                .FirstOrDefault();

            if (userFound != null)
            {
                return BadRequest($"Esse email de usuário já existe");
            }

            await this.Users.InsertOneAsync(newUser);
            return CreatedAtRoute("showUser",
                new { id = newUser.Id.ToString() },
                newUser
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id,
            UpdateUserViewModel model)
        {
            var userFound = await this.Users
                .Find<User>(user => user.Id == id)
                .FirstOrDefaultAsync();

            if (userFound == null)
            {
                return NotFound(id);
            }

            var updated = model.ParseToUser(userFound);

            await this.Users
                .ReplaceOneAsync(x => x.Id == id, updated);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Destroy(string id)
        {
            var userFound = await this.Users
                .FindOneAndDeleteAsync(user => user.Id == id);

            if (userFound == null)
            {
                return NotFound("Usuário não encontrado");
            }

            return NoContent();
        }
    }
}
