

using AuthService.Domain;
using AuthService.Domain.Repository;
using AuthService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repository;   

    /// <summary>
    /// User repository for managing user data in the database.
    /// </summary>
    /// <remarks>
    /// This class implements IUserRepository and provides methods to add, delete, and retrieve users.    
    /// </remarks>;
public sealed class UserRepository : IUserRepository
{
    private readonly AuthDbContext _dbContext;
    public UserRepository(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    /// <summary>
    /// Adds a new user to the database.
    /// </summary>
    /// <param name="user"></param>
    public void Add(UserEntity user)
    {
        _dbContext.Set<UserEntity>().Add(user);
    }

    /// <summary>
    /// Deletes a user from the database.
    /// </summary>
    /// <param name="user"></param>
    public void Delete(UserEntity user)
    {
        _dbContext.Set<UserEntity>().Remove(user);
    }   

    /// <summary>
    /// Gets a user by their email address.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await _dbContext.Set<UserEntity>().FirstOrDefaultAsync(u => u.Email == email);
    }

   /// <summary>
   /// Checks if the email is unique (not already taken) in the database.
   /// </summary>
   /// <param name="email"></param>
   /// <returns></returns>
    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        var result = await _dbContext.Set<UserEntity>()
                                       .Where(x => x.Email.ToLower() == email.ToLower()
                                       ).Select(x => x.Email).FirstOrDefaultAsync();
        return result is not null;
    }     

}