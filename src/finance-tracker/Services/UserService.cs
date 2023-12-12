public class UserService {
    public readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext) {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
}