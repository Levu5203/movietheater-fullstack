import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'customFormat',
})
export class CustomFormatPipe implements PipeTransform {
  transform(value: any, type: string): any {
    if (!value && value !== false) return;

    switch (type) {
      case 'year':
        return new Intl.DateTimeFormat('en-US', { year: 'numeric' }).format(
          new Date(value)
        );
      case 'date':
        return new Intl.DateTimeFormat('en-US', { dateStyle: 'medium' }).format(
          new Date(value)
        );
      case 'datetime':
        return new Intl.DateTimeFormat('en-US', {
          dateStyle: 'short',
          timeStyle: 'short',
        }).format(new Date(value));
      case 'time':
        return new Intl.DateTimeFormat('en-US', { timeStyle: 'short' }).format(
          new Date(value)
        );
      case 'timeShort':
        if (typeof value === 'string' && /^\d{2}:\d{2}(:\d{2})?$/.test(value)) {
          // Use base date to convert time-only string
          value = `1970-01-01T${value}`;
        }
        const date = new Date(value);
        if (isNaN(date.getTime())) return 'Invalid time';
        return new Intl.DateTimeFormat('en-US', {
          hour: '2-digit',
          minute: '2-digit',
          hour12: false,
        }).format(date);

      case 'currency':
        return new Intl.NumberFormat('vi-VN', {
          style: 'currency',
          currency: 'VND',
        }).format(value);
      case 'number':
        return new Intl.NumberFormat('vi-VN').format(value);
      case 'percentage':
        return `${value}%`;
      case 'uppercase':
        return value.toUpperCase();
      case 'lowercase':
        return value.toLowerCase();
      case 'active':
        return value ? 'Active' : 'Inactive';
      default:
        return value;
    }
  }
}
