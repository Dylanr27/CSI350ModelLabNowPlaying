# Models Lab
## Objectives: Students will be able to
- Review Model Binding
- Create one-many relationship 

## Pre-configuration 
1. Update the connection string in appsetting.json to your local server 
2. Run update-database. The migrations should already be created.
3. Verify the Database is set up and that you can run the application.  

## Review Model Binding
Asp .Net Core MVC performs model binding automatically for us by binding the data from an HTTP POST request, route parameter, or query parameter. As we build out the shopping cart feature, we will explore what Model Binding is happening under the hood. 

## Create Tickets
We will create a page that will allow our users to buy tickets. 
For now, we will skip adding any datetime-specific information.
Ticket should have a one-to-many relationship with movies. A ticket will belong to one movie, and movies will have many tickets. 
Tickets will also belong to a user, and a user will have many tickets. What kind of relationship is that?

1. Add a new Model called Ticket. Give it the following properties.  
    ```
        public int Id { get; set; }
        public int Count { get; set; }
        public string MoveId { get; set; }
        [ForeignKey("MovieId")]
        [ValidateNever]
        public Movie Movie { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public IdentityUser ApplicationUser { get; set; } 
   ```
2. add `public ICollection<Ticket> Tickets { get; }` to the Movie model. 
 3. In Application DBContext add `public DbSet<Ticket> Tickets { get; set; }` add the following to  OnModelCreating
       ```
            modelBuilder.Entity<Movie>()
            .HasMany(e => e.Tickets)
            .WithOne(e => e.Movie)
            .HasForeignKey(e => e.MovieId)
            .HasPrincipalKey(e => e.Id);

       ```
4. Add a new migration called AddingTikets through the Package Manager Console and Update the Database
5. Right-click the controllers folder and select add controller. 
6. Select MVC Controller with views, Using Entity Framework
7. Select Ticket as the model Class and ApplicationDbContext as the DbContext. Assure Generate views, Reference script libraries, and Use a Layout page are checked. Verify the controller name is TicketsController and click Add.
8.In the Movie Details, add a link to the Ticket Create Page `<a class="btn btn-primary btn-lg" asp-controller="Tickets" asp-action="Create" >Buy Ticket</a>` In the Ticet Views Create Page change the value on the submit button to `Buy Tickets`  
9. In order to get the UserId, we will need to make sure the Create Ticket route is protected. In the Ticket controller, add [Authorize] above the Create action. Run your code. Navigate to the Movie Detail page and attempt to buy a ticket. If this step is successful, it should direct you to the login/registration page. 
    Note: When we navigate to the detail page, ASP .NET Core performs model binding using the id route parameter 
    Note: When we create a ticket (or perform any other creation), ASP .NET Core performs model binding. Binding the properties in our create Action to the properties from our HTTP Post and form body.
10. We will need to make a few changes to the index Action to display only the tickets our user bought. In the Tickets controller, update the Index Action to the following:
    ```
       [Authorize]
       public async Task<IActionResult> Index()
        {
            //Gets the user ID
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            //Gets the tickets belonging to this user and includes the movies 
            var applicationDbContext = _context.Tickets.Where(r => r.ApplicationUserId == userId).Include(t => t.Movie); ;

            return View(await applicationDbContext.ToListAsync());
        }
    ```
 11. Add a link to the User's tickets to the Movies Index View `<p> < an asp-action="Create">Create New</a> </p>.`
12. Test out your code by creating two accounts. Buy Tickets on one account, view the Tickets Index page, and verify you see Ticket (s) belonging to that user. Log out and Log in with the second account. Navigate to the Tickets Index page and verify you don't see any tickets from the first user. 