

using PetShoop.Application.DTOs;
using PetShoop.Domain.Entities;

namespace PetShoop.Application.Mappings;

public static class PetDTOMappingExtensions
{
    public static PetDto? ToPetDto(this Pet pet)
    {
        if (pet is null)
            return null;

        return new PetDto
        {
            PetId = pet.PetId,
            Nome = pet.Nome,
            Especie = pet.Especie,
            Raca = pet.Raca,
            Sexo = pet.Sexo,
            DataDeNascimento = pet.DataDeNascimento,
            Peso = pet.Peso,
            Cor = pet.Cor,
            Porte = pet.Porte,
            Observacoes = pet.Observacoes,
            ClienteId = pet.ClienteId
        };
    }




    public static Pet? ToPet(this PetDto petDto)
    {
        if (petDto is null) return null;

        var pet = new Pet(
            petDto.Nome,
            petDto.Especie,
            petDto.Raca,
            petDto.Sexo,
            petDto.DataDeNascimento,
            petDto.Peso,
            petDto.Cor,
            petDto.Porte,
            petDto.Observacoes,
            petDto.ClienteId);

        if (petDto.PetId != Guid.Empty)
        {
            pet.SetPetId(petDto.PetId);
        }

        return pet;
    }




    public static IEnumerable<PetDto> ToPetDtoList(this IEnumerable<Pet> pets)
    {
        if (pets is null || !pets.Any())
        {
            return new List<PetDto>();
        }

        return pets.Select(pet => new PetDto
        {
            PetId = pet.PetId,
            Nome = pet.Nome,
            Especie = pet.Especie,
            Raca = pet.Raca,
            Sexo = pet.Sexo,
            DataDeNascimento = pet.DataDeNascimento,
            Peso = pet.Peso,
            Cor = pet.Cor,
            Porte = pet.Porte,
            Observacoes = pet.Observacoes,
            ClienteId = pet.ClienteId
        }).ToList();
    }

}
