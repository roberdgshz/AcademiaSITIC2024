let colores: string[] = ['Rojo', 'Verde', 'Azul'];

interface Articulo {
    nombre: string,
    existencia: number,
    colores: string[],
    marca?: string
}

const refaccion: Articulo = {
    nombre: 'Filtro',
    existencia: 50,
    colores: ['Rojo', 'Negro']
}

refaccion.marca = 'LTH';

console.log(refaccion);
console.table(refaccion);

export {};