using InventoryManagement.Domain.Enums;
using System.Globalization;

namespace InventoryManagement.Application.Validators
{
    public static class InventoryMovementFilterValidator
    {
        public static (DateTime? startDate, DateTime? endDate) ValidateDates(string? startDate, string? endDate)
        {
            DateTime? parsedStartDate = null;
            DateTime? parsedEndDate = null;

            // Validar formato de startDate si existe y convertir a UTC
            if (!string.IsNullOrEmpty(startDate))
            {
                if (!DateTime.TryParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tempStartDate))
                {
                    throw new ArgumentException("Invalid startDate format. Expected format: yyyy-MM-dd.");
                }
                parsedStartDate = DateTime.SpecifyKind(tempStartDate, DateTimeKind.Utc);
            }

            // Validar formato de endDate si existe y convertir a UTC
            if (!string.IsNullOrEmpty(endDate))
            {
                if (!DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tempEndDate))
                {
                    throw new ArgumentException("Invalid endDate format. Expected format: yyyy-MM-dd.");
                }
                parsedEndDate = DateTime.SpecifyKind(tempEndDate, DateTimeKind.Utc);
            }

            // Validar que startDate no sea mayor que endDate
            if (parsedStartDate.HasValue && parsedEndDate.HasValue && parsedStartDate > parsedEndDate)
            {
                throw new ArgumentException("Start date cannot be greater than end date.");
            }

            return (parsedStartDate, parsedEndDate);
        }

        public static InventoryMovementType? ValidateType(string? type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return null; // Si no hay filtro de tipo, se permite cualquier movimiento.
            }

            if (!Enum.TryParse(type.Trim(), true, out InventoryMovementType movementType))
            {
                throw new ArgumentException("Invalid movement type. Allowed values: In, Out.");
            }

            return movementType;
        }
    }
}
