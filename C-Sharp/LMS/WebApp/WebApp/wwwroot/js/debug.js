// Debug utility for LMS WebApp
(function(window) {
    'use strict';

    const DEBUG_MODE = true; // Set to false in production

    window.LMSDebug = {
        log: function(message, data) {
            if (DEBUG_MODE) {
                console.log(`[LMS Debug] ${message}`, data || '');
            }
        },

        error: function(message, error) {
            if (DEBUG_MODE) {
                console.error(`[LMS Error] ${message}`, error || '');
            }
        },

        apiTest: async function(url, method = 'GET') {
            if (!DEBUG_MODE) return;

            try {
                this.log(`Testing API endpoint: ${method} ${url}`);
                const response = await fetch(url, { method });
                this.log(`API Response: ${response.status} ${response.statusText}`);

                if (response.ok) {
                    const data = await response.json();
                    this.log('API Response Data:', data);
                    return data;
                } else {
                    const errorText = await response.text();
                    this.error('API Error Response:', errorText);
                    return null;
                }
            } catch (error) {
                this.error('API Test Failed:', error);
                return null;
            }
        },

        checkElements: function(elementIds) {
            if (!DEBUG_MODE) return;

            elementIds.forEach(id => {
                const element = document.getElementById(id);
                if (element) {
                    this.log(`Element found: #${id}`, element);
                } else {
                    this.error(`Element NOT found: #${id}`);
                }
            });
        },

        checkConfig: function() {
            if (!DEBUG_MODE) return;

            this.log('Configuration Check:');
            this.log('apiConfig available:', typeof apiConfig !== 'undefined');
            if (typeof apiConfig !== 'undefined') {
                this.log('apiConfig.baseUrl:', apiConfig.baseUrl);
            }

            // Check server-side config injection
            const serverConfig = document.querySelector('script[data-api-config]');
            if (serverConfig) {
                this.log('Server config found:', serverConfig.dataset.apiConfig);
            } else {
                this.log('No server config found in DOM');
            }
        }
    };

    // Auto-run configuration check on page load
    if (DEBUG_MODE) {
        document.addEventListener('DOMContentLoaded', function() {
            window.LMSDebug.checkConfig();
        });
    }

})(window);
