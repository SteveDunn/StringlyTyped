# StringlyTyped.ValueObjects
<p align="center">
  <img src="./assets/cavey.png">
</p>

# NOTE:
**For an alternative, more performant approach to Value Objects, please see https://github.com/SteveDunn/Vogen**

### A cure for StringlyTyped software and PrimitiveObsession.

Primitive Obsession AKA **StringlyTyped** means being obsessed with primitives.  It is a Code Smell that degrades the quality of software.

> "*Primitive Obsession is using primitive data types to represent domain ideas*" [#](https://wiki.c2.com/?PrimitiveObsession)


# What is the repository?
It's a very simple implementation of a **ValueObject**. ([#](https://wiki.c2.com/?ValueObject))

A ValueObject is a strongly typed (*strongly, not stringly*) domain object that is immutable.

Instead of
```
int customerId = 42;
```

... we have

```
var customerId = CustomerId.From(42);
```

`CustomerId` derives from this package's `ValueObject` type:
``` cs
public class CustomerId : ValueObject<int, CustomerId>
{
}
```

Here it is again with some validation

``` cs
public class CustomerId : ValueObject<int, CustomerId>
{
    public override Validation Validate() => Value > 0 
      ? Validation.Ok 
      : Validation.Invalid("Customer IDs cannot be zero or negative.");
}
```

This allows us to have more _strongly typed_ domain objects instead of primitives, which makes the code easier to read and enforces better method signatures, so instead of:

``` cs
public void DoSomething(int customerId, int supplierId, int amount)
```
we can have:

``` cs
public void DoSomething(CustomerId customerId, SupplierId supplierId, Amount amount)
```

Now, callers can't mess up the ordering or parameters and accidentally pass us a Supplier ID in place of a Customer ID.

It also means that validation **is in just one place**. You can't introduce bad objects into your domain, therefore you can assume that **in _your_ domain** every ValueObject is valid.  Handy.

# Tell me more about the Code Smell
There's a blog post [here](https://dunnhq.com/posts/2021/primitive-obsession/) that describes it in more detail.  I'll recap here:

Primitive Obsession is being *obsessed* with the *seemingly* **convenient** way that primitives, such as `ints` and `strings`, allow us to represent domain objects and ideas.

It is **this**:
``` cs
int customerId = 42
```

What's wrong with that?

A customer ID likely **cannot** be *fully* represented by an `int`.  An `int` can be negative or zero, but it's unlikely a customer ID can be. So, we have **constraints** on a customer ID.  We can't _represent_ or _enforce_ those constraints on an `int`.

So, we need some validation to ensure the **constraints** of a customer ID are met. Because it's in `int`, we can't be sure if it's been checked beforehand, so we need to check it every time we use it.  Because it's a primitive, someone might've changed the value, so even if we're 100% sure we've checked it before, it still might need checking again.

So far, we've used as an example, a customer ID of value `42`.  In C#, it may come as no surprise that "`42 == 42`" (*I haven't checked that in JavaScript!*).  But, in our **domain**, should `42` always equal `42`?  Probably not if you're comparing a Supplier ID of `42` to a Customer ID of `42`! But primitives won't help you here (remember, `42 == 42`!) Given this signature:

``` cs
public void DoSomething(int customerId, int supplierId, int amount)
```

.. the compiler won't tell you you've messed it up by accidentally swapping customer and supplier IDs.

But by using ValueObjects, that signature becomes much more strongly typed:

``` cs
public void DoSomething(CustomerId customerId, SupplierId supplierId, Amount amount)
```

Now, the caller can't mess up the ordering of parameters, and the objects themselves are guaranteed to be valid and immutable.
