using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecTask.Migrations
{
    /// <inheritdoc />
    public partial class CreateStoredProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE OR REPLACE FUNCTION public.GetBMIStatisticsByAge()
    RETURNS TABLE (
        AgeRange VARCHAR(50),
        BMICharacteristic VARCHAR(50),
        Percentage NUMERIC
    )
AS $$
BEGIN
    RETURN QUERY
    SELECT
        CASE
            WHEN age >= 0 AND age <= 10 THEN '0-10'
            WHEN age >= 11 AND age <= 20 THEN '11-20'
            WHEN age >= 21 AND age <= 30 THEN '21-30'
            WHEN age >= 31 AND age <= 40 THEN '31-40'
	    WHEN age >= 41 AND age <= 50 THEN '41-50'
	    WHEN age >= 51 AND age <= 60 THEN '51-60'
	    WHEN age >= 61 AND age <= 70 THEN '61-70'
	    WHEN age >= 71 AND age <= 80 THEN '71-80'
	    WHEN age >= 81 AND age <= 90 THEN '81-90'
	    WHEN age >= 91 AND age <= 100 THEN '91-100'
	    WHEN age >= 101 AND age <= 110 THEN '101-110'
	    WHEN age >= 111 AND age <= 120 THEN '111-120'
	    WHEN age >= 121 AND age <= 125 THEN '121-125'
        END AS AgeRange,
        BMICharacteristic,
        (COUNT(*) * 100.0) / SUM(COUNT(*)) OVER (PARTITION BY AgeRange) AS Percentage
    FROM
        patient
    GROUP BY
        AgeRange,
        BMICharacteristic
    ORDER BY
        AgeRange,
        Percentage DESC;
END;
$$ LANGUAGE plpgsql;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop function GetBMIStatisticsByAge");
        }
    }
}
