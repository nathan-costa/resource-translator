This utility creates a key/value across all resource files for a given string, the key remains the same and the value is translated to the appropriate language.

Currently it scrapes the translations from a site which uses the Google Translate API, but I have tried to make it extensible so that Google API can be used directly if a developer license is aquired. 

Selenium server is required to use the TranslationScraper class.

If a given key exists in any of the current resource files, no data will be written.