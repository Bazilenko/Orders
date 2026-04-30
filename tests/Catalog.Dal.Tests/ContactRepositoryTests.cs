using FluentAssertions;
using Catalog.Dal.Entities;
using Catalog.Dal.Repositories;
using Catalog.Dal.Context;
using Xunit;

namespace Catalog.Dal.Tests;

public class ContactRepositoryTests : IDisposable
{
    private readonly MyDbContext _context;
    private readonly ContactRepository _sut;

    public ContactRepositoryTests()
    {
        _context = DbContextFactory.Create();
        _sut = new ContactRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ValidContact_ShouldPersistInDatabase()
    {
        // Arrange
        var contact = TestDataBuilder.CreateContact(value: "test@email.com", type: "Email");

        // Act
        await _sut.AddAsync(contact);
        await _context.SaveChangesAsync();

        // Assert
        var result = await _sut.GetByIdAsync(contact.Id);
        result.Should().NotBeNull();
        result!.Value.Should().Be("test@email.com");
        result.Type.Should().Be("Email");
    }

    [Fact]
    public async Task UpdateAsync_ExistingContact_ShouldChangeValues()
    {
        // Arrange
        var contact = TestDataBuilder.CreateContact(value: "111", type: "Phone");
        await _sut.AddAsync(contact);
        await _context.SaveChangesAsync();

        // Act
        contact.Value = "222";
        await _sut.UpdateAsync(contact);
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _sut.GetByIdAsync(contact.Id);
        updated!.Value.Should().Be("222");
    }

    [Fact]
    public async Task DeleteAsync_ExistingContact_ShouldRemoveFromDatabase()
    {
        // Arrange
        var contact = TestDataBuilder.CreateContact();
        await _sut.AddAsync(contact);
        await _context.SaveChangesAsync();

        // Act
        await _sut.DeleteAsync(contact);
        await _context.SaveChangesAsync();

        // Assert
        var result = await _sut.GetByIdAsync(contact.Id);
        result.Should().BeNull();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}