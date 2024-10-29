interface AudioPlayer {
    audioVolume: number;
    songDuration: number;
    song: string;
    details: Details;
}

interface Details {
    author: string;
    year: number;
}

const audioPlayer: AudioPlayer = {
    audioVolume: 90,
    songDuration: 36,
    song: 'Misery Business',
    details: {
        author: 'Hayley Williams',
        year: 2007
    }
};

console.log('song', audioPlayer.song)
console.log('duracion', audioPlayer.songDuration)
console.log('audioVolume', audioPlayer.audioVolume)

const { song: anotherSong, songDuration: duracion, audioVolume, details: { author} } = audioPlayer;
console.log({ anotherSong, duracion, audioVolume });

const song = 'New song';

// EJERCICIO: Desestructurar el autor.
// Recordando que el autor se encuentra dentro de 'details'
const { details } = audioPlayer;
// const { author } = details;

console.log({ author })

// Desestructuración de Arreglos

// const team7: string[] = ['Naruto', 'Sasuke', 'Sakura'];
// console.log('Personaje 3', team7[3] || 'No encontrado');

// const sakura = team7[3] || 'No encontrado';
// console.log('Personaje 3', sakura);

// const [naruto, sasuke, sakura]: string[] = ['Naruto', 'Sasuke', 'Sakura'];
const [, , sakura = 'No encontrado' ]: string[] = ['Naruto', 'Sasuke'];
console.log(sakura);

export {};