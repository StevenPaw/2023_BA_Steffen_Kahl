<?php

namespace App\CharacterDatabase;

use SilverStripe\Assets\Image;
use SilverStripe\ORM\DataObject;
use App\Users\UserData;
use SilverStripe\Security\Permission;

/**
 * Class \App\Database\Experience
 *
 * @property string $Title
 * @property int $ImageID
 * @method \SilverStripe\Assets\Image Image()
 */
class CharacterHat extends DataObject
{
    private static $db = [
        "Title" => "Varchar(255)",
    ];

    private static $has_one = [
        "Image" => Image::class,
    ];

    private static $belongs_many = [
        "UserData" => UserData::class,
    ];

    private static $summary_fields = [
        "Title" => "Title",
    ];

    private static $field_labels = [
        "Title" => "Title",
    ];

    private static $default_sort = "ID ASC";

    private static $table_name = "CharacterHat";

    private static $singular_name = "Hat";
    private static $plural_name = "Hats";

    private static $url_segment = "character-hat";

    public function getCMSFields()
    {
        $fields = parent::getCMSFields();
        return $fields;
    }

    public function canView($member = null)
    {
        return true;
    }

    public function canEdit($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canDelete($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canCreate($member = null, $context = [])
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }
}
