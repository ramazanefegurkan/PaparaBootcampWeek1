using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductDbContext _context;
    private readonly IValidator<Product> _productValidator;
    public ProductsController(ProductDbContext context, IValidator<Product> productValidator)
    {
        _context = context;
        _productValidator = productValidator;
    }

    // Retrieves a product by its ID
    [HttpGet("{id}")]
    public ActionResult<ApiResponse<Product>> Get(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound(ApiResponse<Product>.ErrorResult("Product not found"));
        }
        return Ok(ApiResponse<Product>.SuccessResult(product));
    }

    // Lists all products or filters by name if the 'name' query parameter is provided
    [HttpGet("list")]
    public ActionResult<ApiResponse<List<Product>>> List([FromQuery] string? name)
    {
        List<Product> products;
        if (string.IsNullOrEmpty(name))
        {
            products = _context.Products.ToList();
        }
        else
        {
            products = _context.Products.Where(p => p.Name.Contains(name)).ToList();
        }
        return Ok(ApiResponse<List<Product>>.SuccessResult(products));
    }

    // Sorts products by price in ascending or descending order based on the 'ascending' query parameter
    [HttpGet("sortByPrice")]
    public ActionResult<ApiResponse<List<Product>>> SortByPrice([FromQuery] bool ascending = true)
    {
        List<Product> sortedProducts;
        if (ascending)
        {
            sortedProducts = _context.Products.OrderBy(p => p.Price).ToList();
        }
        else
        {
            sortedProducts = _context.Products.OrderByDescending(p => p.Price).ToList();
        }
        return Ok(ApiResponse<List<Product>>.SuccessResult(sortedProducts));
    }

    // Creates a new product
    [HttpPost]
    public ActionResult<ApiResponse<Product>> Create([FromBody] Product product)
    {
        // Validate the product using FluentValidation
        ValidationResult validationResult = _productValidator.Validate(product);
        if (!validationResult.IsValid)
        {
            return BadRequest(ApiResponse<Product>.ErrorResult(validationResult.ToString("~")));
        }

        // Add the product to the context and save changes
        _context.Products.Add(product);
        _context.SaveChanges();

        return CreatedAtAction(nameof(Get), new { id = product.Id }, ApiResponse<Product>.SuccessResult(product, "Product created successfully"));
    }

    // Updates an existing product by its ID
    [HttpPut("{id}")]
    public ActionResult<ApiResponse<Product>> Update(int id, [FromBody] Product updatedProduct)
    {
        // Validate the updated product using FluentValidation
        ValidationResult validationResult = _productValidator.Validate(updatedProduct);
        if (!validationResult.IsValid)
        {
            return BadRequest(ApiResponse<Product>.ErrorResult(validationResult.ToString("~")));
        }

        // Find the product by ID
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound(ApiResponse<Product>.ErrorResult("Product not found"));
        }

        // Update the product properties
        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Category = updatedProduct.Category;
        product.Price = updatedProduct.Price;
        product.Stock = updatedProduct.Stock;

        // Save the changes to the context
        _context.SaveChanges();

        return Ok(ApiResponse<Product>.SuccessResult(product, "Product updated successfully"));
    }

    // Deletes a product by its ID
    [HttpDelete("{id}")]
    public ActionResult<ApiResponse<bool>> Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound(ApiResponse<bool>.ErrorResult("Product not found"));
        }

        // Remove the product from the context and save changes
        _context.Products.Remove(product);
        _context.SaveChanges();
        return Ok(ApiResponse<bool>.SuccessResult(true, "Product deleted successfully"));
    }
}