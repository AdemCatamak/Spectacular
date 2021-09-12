# Spectacular

This library is implementation of the Specification Design Pattern in C# language.

`Expression` has been used in this implementation. Thanks to `Expression`, developers can use `Specification` for
querying database through libraries such as `EntityFramework`.

You can combine `Specification` classes through the `And` operator included as built-In. Via these operator,
you can easily create your complex queries by combining smaller criteria.

For more information about the Specification Design Pattern, you can check the [story on medium.](https://medium.com/c-sharp-progarmming/specification-design-pattern-c814649be0ef)

| Platform | Status |
| ------- | ----- |
| `Travis` | ![Travis](https://travis-ci.com/AdemCatamak/Spectacular.svg?branch=master) |
| `GitHub` | ![.github/workflows/github.yml](https://github.com/AdemCatamak/Spectacular/workflows/.github/workflows/github.yml/badge.svg?branch=master) |


| NuGet Package Name | Version |
| ------- | ----- |
| Spectacular | ![Nuget](https://img.shields.io/nuget/v/Spectacular.svg) | 


## Give a Star! :star:
If you like this project please give it a star :blush:

## Getting Started

Preparing custom specification via `AbstractSpecification`

```CSharp
public class GenderShould : AbstractSpecification<Person>
{
    private GenderShould(Genders gender)
        : base(person => person.Gender == gender)
    {
    }

    public static AbstractSpecification<Person> Be(Genders gender)
    {
        GenderShould genderShould = new(gender);
        return genderShould;
    }
    
    public static AbstractSpecification<Person> BeMale => Be(Genders.Male);
    public static AbstractSpecification<Person> BeFemale => Be(Genders.Female);
}
```

Checking whether an object satisfy the criteria :

```CSharp
Person person = new("adem", Genders.Male);

AbstractSpecification<Person> spec = GenderShould.BeMale;
bool isSatisfied = spec.IsSatisfiedBy(person);
```

Filtering the collection by criteria :

```CSharp
IQueryable<Person> personCollection = ...

AbstractSpecification<Person> spec = GenderShould.BeMale;
var filteredCollection = personCollection.Where(spec).ToList();
```

Combine criteria : 

```CSharp
var ageCriteria = AgeShould.BeGreaterThan(18).And(AgeShould.BeLessThan(35));
SpecificationGroup<Person> specificationGroup = ageCriteria.And(GenderShould.BeFemale);

var filteredCollection = personCollection.Where(specificationGroup).ToList();
```
