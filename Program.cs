//Fixing that in 3 ways:

// Declare the variable as nullable with '?'
// This tells the compiler: "I know this might be null. I accept responsibility."
string? region = null;

// Null-conditional operator '?.' — skip the call if null
// If region is null, ToUpper() never executes. No crash.
// Output:"Region (conditional):"
string? upperRegion = region?.ToUpper();
Console.WriteLine($"Region (conditional): {upperRegion}");

// Null-coalescing operator '??' — provide a fallback value
// If region is null, use "Unassigned" instead.
// Output: "Region (coalesced): Unassigned"
string displayRegion = region ?? "Unassigned";
Console.WriteLine($"Region (coalesced): {displayRegion}");

// Null-coalescing assignment '??=' — assign only if currently null
// Useful for lazy initialization.
// Output: "Region (assigned): Addis Ababa"
region ??= "Addis Ababa";
Console.WriteLine($"Region (assigned): {region}");

