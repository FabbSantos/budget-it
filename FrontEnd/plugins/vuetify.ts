import { createVuetify} from 'vuetify';
import type { ThemeDefinition} from 'vuetify'; 
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';

const myTheme: ThemeDefinition = {
    dark: false,
    colors: {
        primaryPink: '#D64BC2',
        secondaryPink: '#FFD4F4',
        incomeGreen: '#00AE4E',
        expenseRed: '#FF4437',
        gray: '#F1F3F4',
        label: '#001733',
        heading: '#25282C',
    },
};
const myThemeDark: ThemeDefinition = {
    dark: true,
    colors: {
        primaryPink: '#D64BC2',
        secondaryPink: '#FFD4F4',
        incomeGreen: '#00AE4E',
        expenseRed: '#FF4437',
        gray: '#1A2023',
        label: '#5E6D80',
        heading: '#EEF2F7',
    },
};

export default defineNuxtPlugin(nuxtApp =>{
    const vuetify = createVuetify({
        components,
        directives,
        theme: {
            themes: {
                light: myTheme,
                dark: myThemeDark,
            },
        }
    });

    nuxtApp.vueApp.use(vuetify);
});