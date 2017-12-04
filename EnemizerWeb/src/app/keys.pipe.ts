import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'keys'
})
export class KeysPipe implements PipeTransform
{
    transform (value, args: string[]): any
    {
        let keys = [];
        for (let enumMember in value)
        {
            if (value.hasOwnProperty(enumMember))
            {
                let isValueProperty = parseInt(enumMember, 10) >= 0;
                if (isValueProperty)
                {
                    keys.push({ key: parseInt(enumMember, 10), value: value[parseInt(enumMember, 10)] });
                    //console.log("enum member: ", value[parseInt(enumMember, 10)]);
                }
            }
        }
        return keys;
    }
}
