<?php

namespace {

use SilverStripe\Security\Member;
use SilverStripe\Security\BasicAuth;
use SilverStripe\Control\HTTPRequest;

    use SilverStripe\CMS\Controllers\ContentController;

    /**
 * Class \PageController
 *
 * @property \Page dataRecord
 * @method \Page data()
 * @mixin \Page
 */
    class PageController extends ContentController
    {
        /**
         * An array of actions that can be accessed via a request. Each array element should be an action name, and the
         * permissions or conditions required to allow the user to access it.
         *
         * <code>
         * [
         *     'action', // anyone can access this action
         *     'action' => true, // same as above
         *     'action' => 'ADMIN', // you must have ADMIN permissions to access this action
         *     'action' => '->checkAction' // you can only access this action if $this->checkAction() returns true
         * ];
         * </code>
         *
         * @var array
         */
        private static $allowed_actions = [
            "account"
        ];

        protected function init()
        {
            parent::init();
            // You can include any CSS or JS required by your project here.
            // See: https://docs.silverstripe.org/en/developer_guides/templates/requirements/
        }

        public function account(HTTPRequest $request)
        {
            $data['API_Title'] = "Nordland Games API";
            $data['API_Description'] = "This API enables games to access the Nordland Games database.";
            $data['API_Version'] = "1.0.0";

            $data['Copyright'] = "API developed and maintained by Steffen Kahl. All rights reserved.";

            $this->response->addHeader('Content-Type', 'application/json');
            return json_encode($data);

            $member = BasicAuth::requireLogin($request, false);
            if ($member instanceof Member) {
                $member->setCurrentUser();
                $data['API_Title'] = "Nordland Games API";
                $data['API_Description'] = "This API enables games to access the Nordland Games database.";
                $data['API_Version'] = "1.0.0";

                $data['Copyright'] = "API developed and maintained by Steffen Kahl. All rights reserved.";

                $this->response->addHeader('Content-Type', 'application/json');
                return json_encode($data);
            }
        }
    }
}
