module.exports = {
    content: ['./**/*.{razor,html}'],
    darkMode: 'class',
    theme: {
        extend: {
            gridTemplateColumns: {
                '16': 'repeat(16, minmax(0, 1fr))',
            },

            gridColumn: {
                'span-16': 'span 16 / span 16',
                'span-15': 'span 15 / span 15',
            },
        },
        colors: {
            'crimson': '#B12D2D',
            'coral': '#BA4747',
            'rose': '#C46161',
            'pink': '#D89696',
            'skyblue': '#C6D8FF',
            'blue': '#226CE0',
            'cyan': '#91B6F0',
            'white': '#F6EDE4',
        },
    },
    plugins: [],
}